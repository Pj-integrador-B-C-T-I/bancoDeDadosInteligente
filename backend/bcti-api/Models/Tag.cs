using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BancoDeConhecimentoInteligenteAPI.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        // Navegação para ArticleTag
        public ICollection<ArticleTag> ArticleTags { get; set; } = new List<ArticleTag>();
    }
}
