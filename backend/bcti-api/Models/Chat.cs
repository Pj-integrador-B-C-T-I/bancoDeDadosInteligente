using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BancoDeConhecimentoInteligenteAPI.Models
{
    public class Chat
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public Usuario User { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = "Novo Chat";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Relacionamento 1:N com perguntas
        public ICollection<Question> Questions { get; set; } = new List<Question>();
    }
}
