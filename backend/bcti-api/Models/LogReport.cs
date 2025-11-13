using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BancoDeConhecimentoInteligenteAPI.Models
{
    
    public class LogReport
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string SystemAffected { get; set; }
        public string ErrorType { get; set; }
        public string Resolution { get; set; }

        [ForeignKey("Author")]
        public int? AuthorId { get; set; }
        public Usuario Author { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }

}
