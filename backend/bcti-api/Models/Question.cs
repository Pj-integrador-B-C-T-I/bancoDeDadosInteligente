using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BancoDeConhecimentoInteligenteAPI.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ChatId { get; set; }

        [ForeignKey("ChatId")]
        public Chat Chat { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Relacionamento 1-1 com Answer
        public Answer Answer { get; set; }
    }
}
