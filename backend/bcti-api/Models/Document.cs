using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BancoDeConhecimentoInteligenteAPI.Models
{
    public class Document
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Article")]
        public int ArticleId { get; set; }
        public Article Article { get; set; }

        [Required]
        public string FileName { get; set; } = string.Empty;

        [Required]
        public string FilePath { get; set; } = string.Empty;

        public string FileType { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
