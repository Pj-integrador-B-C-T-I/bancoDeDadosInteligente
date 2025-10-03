using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BancoDeConhecimentoInteligenteAPI.Models
{
    public class ChatHistory
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public Usuario User { get; set; }

        public string Question { get; set; }
        public string Answer { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    }
}