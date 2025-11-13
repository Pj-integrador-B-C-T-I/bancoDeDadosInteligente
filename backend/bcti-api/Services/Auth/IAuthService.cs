using BancoDeConhecimentoInteligenteAPI.Dtos.Auth;
using BancoDeConhecimentoInteligenteAPI.Models;


namespace BancoDeConhecimentoInteligenteAPI.Services
{
    public interface IAuthService
    {
        Task<Usuario> CadastrarAsync(RegisterDto usuarioDto);
        Task<string> LoginAsync(LoginDto loginDto);
        Task EnviarLinkRedefinicaoSenha(string email);
    }
}
