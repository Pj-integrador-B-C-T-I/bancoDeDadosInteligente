namespace BancoDeConhecimentoInteligenteAPI.Dtos
{
    public class EmbeddingDto
    {
        public int Id { get; set; }
        public int? ArticleId { get; set; }
        public string Chunk { get; set; } = string.Empty;
        public float[] Vector { get; set; } = Array.Empty<float>();
        public string Source { get; set; } = string.Empty;
        public int? SourceId { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class EmbeddingCreateDto
    {
        public int? ArticleId { get; set; }
        public string Chunk { get; set; } = string.Empty;
        public float[] Vector { get; set; } = Array.Empty<float>();
        public string Source { get; set; } = "article";
        public int? SourceId { get; set; }
    }
}
