namespace BancoDeConhecimentoInteligenteAPI.Services
{
    public interface IEmailService
    {
        Task EnviarConfirmacao(string email, string nome, string linkConfirmacao);
        Task EnviarEmailAsync(string email, string nome, string titulo, string subtitulo, string textoBotao, string link);
    }
}
