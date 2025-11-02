namespace BancoDeConhecimentoInteligenteAPI.Dtos.ChatMessage
{
    public class CreateChatMessageDto
    {
        public int ChatId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }

    public class ReadChatMessageDto
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
