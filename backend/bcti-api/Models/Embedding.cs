using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BancoDeConhecimentoInteligenteAPI.Models
{
    public class Embedding
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Article")]
        public int? ArticleId { get; set; }
        public Article? Article { get; set; }

        [Required]
        public string Chunk { get; set; } = string.Empty;

        // Vetor serializado como JSON string
        [Required]
        public string VectorJson { get; set; } = string.Empty;

        public string Source { get; set; } = "article"; // article | log | document | etc
        public int? SourceId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
