using System.ComponentModel.DataAnnotations;

namespace BancoDeConhecimentoInteligenteAPI.Models
{
    public class Usuario
    {
        [Key] public int Id { get; set; }
        [Required] public string Nome { get; set; } = string.Empty;
        [Required] public string Email { get; set; } = string.Empty;
        [Required] public string SenhaHash { get; set; } = string.Empty;
        public string Funcao { get; set; } = "adm";
        public bool Ativo { get; set; } = true;
        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    }
}
