namespace BancoDeConhecimentoInteligenteAPI.Dtos.Usuarios
{
    public class ReadUsuarioDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public DateTime CriadoEm { get; set; }
        public string Tipo { get; set; } = "Cliente";
        public bool Ativo { get; set; }
        public bool EmailVerificado { get; set; }
    }

    public class UpdateUsuarioDto
    {
        public string Nome { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Tipo { get; set; } = "Cliente";
        public bool Ativo { get; set; }
    }
}
