namespace BancoDeConhecimentoInteligenteAPI.Dtos.Auth
{
    public class RegisterDto
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;

        public string Telefone { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string Tipo { get; set; } = "Cliente";
    }

    public class LoginDto
    {
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }

    public class AuthResponseDto
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public bool Ativo { get; set; }
        public bool EmailVerificado { get; set; }
        public string Tipo { get; set; } = "Cliente";
        public int Id { get; set; } 
    }

    public class EsqueciMinhaSenhaDto
    {
        public string Email { get; set; }
    }

    public class ResetPasswordDto
    {
        public string Token { get; set; }
        public string NovaSenha { get; set; }
    }
}
