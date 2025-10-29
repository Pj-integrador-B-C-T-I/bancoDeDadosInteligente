using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BancoDeConhecimentoInteligenteAPI.Models
{
    public class ChatMessage
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Chat")]
        public int ChatId { get; set; }
        public Chat Chat { get; set; }

        [Required]
        public string Question { get; set; }

        public string Answer { get; set; }

        public DateTime SentAt { get; set; } = DateTime.UtcNow;
    }

    public class UpdateChatMessageDto
    {
        public string Message { get; set; }
    }

}
