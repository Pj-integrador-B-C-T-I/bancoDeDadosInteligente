using System.ComponentModel.DataAnnotations;

namespace BancoDeConhecimentoInteligenteAPI.Models
{
    public class Usuario
    {
        [Key] 
        public int Id { get; set; }

        [Required] 
        public string Nome { get; set; } = string.Empty;

        [Required] 
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required] 
        public string SenhaHash { get; set; } = string.Empty;

        public string Telefone { get; set; } = string.Empty;

        public string Cpf { get; set; } = string.Empty;

        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;

        public string Tipo { get; set; } = "Cliente"; // ou "Adm"

        public bool Ativo { get; set; } = false;

        public bool EmailVerificado { get; set; } = false;
    }
}
