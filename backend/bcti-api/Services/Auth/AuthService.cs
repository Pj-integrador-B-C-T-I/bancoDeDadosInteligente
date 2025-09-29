using BancoDeConhecimentoInteligenteAPI.Data;
using BancoDeConhecimentoInteligenteAPI.Dtos.Auth;
using BancoDeConhecimentoInteligenteAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BancoDeConhecimentoInteligenteAPI.Services
{

    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly PasswordHasher<Usuario> _passwordHasher;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public AuthService(AppDbContext context, IConfiguration configuration, IEmailService emailService)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<Usuario>();
            _configuration = configuration;
            _emailService = emailService;
        }

        public async Task<Usuario> CadastrarAsync(RegisterDto usuarioDto)
        {
            if (await _context.Usuarios.AnyAsync(u => u.Email == usuarioDto.Email))
                throw new Exception("Email já cadastrado");

            var usuario = new Usuario
            {
                Nome = usuarioDto.Nome,
                Email = usuarioDto.Email,
                Telefone = usuarioDto.Telefone,
                Cpf = usuarioDto.Cpf,
                CriadoEm = DateTime.UtcNow,
                Ativo = false,
                Tipo = usuarioDto.Tipo ?? "Cliente"
            };

            usuario.SenhaHash = _passwordHasher.HashPassword(usuario, usuarioDto.Senha);

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            // 🔑 gerar token de verificação de e-mail
            var token = GerarTokenVerificacaoEmail(usuario);
            var link = $"{_configuration["AppUrl"]}/confirmar-email?token={token}";

            await _emailService.EnviarConfirmacao(usuario.Email, usuario.Nome, link);

            return usuario;
        }

        public async Task<string> LoginAsync(LoginDto loginDto)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == loginDto.Email);

            if (usuario == null || 
                _passwordHasher.VerifyHashedPassword(usuario, usuario.SenhaHash, loginDto.Senha) == PasswordVerificationResult.Failed)
                throw new Exception("Usuário ou senha inválidos");

            if (!usuario.EmailVerificado)
                throw new Exception("Você precisa confirmar seu e-mail antes de fazer login.");

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.Tipo)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(Convert.ToDouble(_configuration["Jwt:ExpirationHours"] ?? "1")),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task EnviarLinkRedefinicaoSenha(string email)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            if (usuario == null) return; // não expõe se existe ou não

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Email, usuario.Email)
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            var link = $"{_configuration["AppUrl"]}/RedefinirSenha?token={Uri.EscapeDataString(tokenString)}";

            await _emailService.EnviarEmailAsync(
                usuario.Email,
                usuario.Nome,
                "Redefina sua senha",
                "Clique no botão abaixo para redefinir sua senha:",
                "Redefinir senha",
                link
            );
        }

        private string GerarTokenVerificacaoEmail(Usuario usuario)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Email, usuario.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
