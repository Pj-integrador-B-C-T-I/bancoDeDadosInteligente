using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BancoDeConhecimentoInteligenteAPI.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        // Relações
        [ForeignKey(nameof(Author))]
        public int AuthorId { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        // Propriedades de navegação
        public virtual Usuario? Author { get; set; }
        public virtual Category? Category { get; set; }

        // Timestamps
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<ArticleTag> ArticleTags { get; set; } = new List<ArticleTag>();
    }
}
