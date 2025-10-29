namespace BancoDeConhecimentoInteligenteAPI.Dtos.ChatHistory
{
    public class CreateChatHistoryDto
    {
        public int UserId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }


    }

    public class ReadChatHistoryDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public DateTime CreateAt { get; set; }
    }

    public class UpdateChatHistoryDto
    {
        public string Answer { get; set; }
    }

}