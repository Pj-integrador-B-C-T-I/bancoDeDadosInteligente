namespace BancoDeConhecimentoInteligenteAPI.Dtos.Chat
{
    public class CreateChatDto
    {
        public int UserId { get; set; }
        public string Title { get; set; }
    }

    public class ReadChatDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class UpdateChatDto
    {
        public string Title { get; set; }
    }

}
