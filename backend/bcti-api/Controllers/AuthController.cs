using BancoDeConhecimentoInteligenteAPI.Data;
using BancoDeConhecimentoInteligenteAPI.Dtos.Auth;
using BancoDeConhecimentoInteligenteAPI.Models;
using BancoDeConhecimentoInteligenteAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Runtime.InteropServices;

namespace BancoDeConhecimentoInteligenteAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly PasswordHasher<Usuario> _passwordHasher;
        private readonly IConfiguration _config;
        private readonly IEmailService _emailService;
        private readonly IAuthService _authService;

        public AuthController(AppDbContext context, IConfiguration config, IEmailService emailService, IAuthService authService)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<Usuario>();
            _config = config;
            _emailService = emailService;
            _authService = authService;
        }

        [HttpPost("cadastrar")]
        public async Task<IActionResult> Cadastrar([FromBody] RegisterDto dto)
        {
            try
            {
                var usuario = await _authService.CadastrarAsync(dto);
                return Ok(new
                {
                    usuario.Id,
                    usuario.Nome,
                    usuario.Email,
                    usuario.Ativo,
                    usuario.EmailVerificado
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            try
            {
                var token = await _authService.LoginAsync(dto);

                var usuario = await _context.Usuarios.FirstAsync(u => u.Email == dto.Email);

                string timeZoneId = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                    ? "E. South America Standard Time"
                    : "America/Sao_Paulo";

                var tz = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
                var dataLocal = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, tz);


                // 📨 Enviar e-mail de aviso de login
                var titulo = "Login realizado com sucesso";
                var subtitulo = $"Detectamos um login na sua conta em {dataLocal:dd/MM/yyyy HH:mm} (horário de Brasília).";
                var textoBotao = "Ver minha conta";
                var link = _config["AppUrl"] ?? "https://alure.app"; // ou qualquer URL que leve para a conta do usuário

                await _emailService.EnviarEmailAsync(
                    usuario.Email,
                    usuario.Nome,
                    titulo,
                    subtitulo,
                    textoBotao,
                    link
                );

                return Ok(new AuthResponseDto
                {
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    Token = token,
                    Ativo = usuario.Ativo,
                    EmailVerificado = usuario.EmailVerificado,
                    Tipo = usuario.Tipo
                });
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpGet("confirmar-email")]
        public async Task<IActionResult> ConfirmarEmail([FromQuery] string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);

            try
            {
                var principal = handler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = _config["Jwt:Issuer"],
                    ValidAudience = _config["Jwt:Audience"],
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                }, out SecurityToken validatedToken);

                var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null) return BadRequest("Token inválido");

                var usuario = await _context.Usuarios.FindAsync(int.Parse(userId));
                if (usuario == null) return NotFound("Usuário não encontrado");

                usuario.Ativo = true;
                usuario.EmailVerificado = true;
                await _context.SaveChangesAsync();

                return Ok("E-mail confirmado com sucesso!");
            }
            catch (SecurityTokenExpiredException)
            {
                return BadRequest("Token expirado.");
            }
            catch
            {
                return BadRequest("Token inválido.");
            }
        }

        [HttpPost("esqueci-minha-senha")]
        public async Task<IActionResult> EsqueciMinhaSenha([FromBody] EsqueciMinhaSenhaDto dto)
        {
            await _authService.EnviarLinkRedefinicaoSenha(dto.Email);
            return Ok("Se o e-mail estiver cadastrado, você receberá um link.");
        }

        [HttpPost("redefinir-senha")]
        public async Task<IActionResult> RedefinirSenha([FromBody] ResetPasswordDto dto)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);

            try
            {
                var principal = handler.ValidateToken(dto.Token, new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero,
                    ValidateLifetime = true
                }, out SecurityToken validatedToken);

                var email = principal.FindFirstValue(ClaimTypes.Email);
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
                if (usuario == null) return BadRequest("Usuário não encontrado");

                usuario.SenhaHash = _passwordHasher.HashPassword(usuario, dto.NovaSenha);
                await _context.SaveChangesAsync();

                return Ok("Senha redefinida com sucesso!");
            }
            catch
            {
                return BadRequest("Token inválido ou expirado.");
            }
        }
    }
}
