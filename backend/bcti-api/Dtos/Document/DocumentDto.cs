namespace BancoDeConhecimentoInteligenteAPI.Dtos
{
    public class DocumentDto
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string FileType { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }

    public class DocumentCreateDto
    {
        public int ArticleId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string FileType { get; set; } = string.Empty;
    }
}
