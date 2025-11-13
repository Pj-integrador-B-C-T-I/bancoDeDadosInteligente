namespace BancoDeConhecimentoInteligenteAPI.Dtos
{
    public class TagDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class TagCreateDto
    {
        public string Name { get; set; } = string.Empty;
    }

    public class ArticleTagDto
    {
        public int ArticleId { get; set; }
        public int TagId { get; set; }
        public string TagName { get; set; } = string.Empty;
    }
}
