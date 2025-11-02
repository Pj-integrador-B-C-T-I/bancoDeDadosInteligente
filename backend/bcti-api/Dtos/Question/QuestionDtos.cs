using System;

namespace BancoDeConhecimentoInteligenteAPI.Dtos.Question
{
    public class QuestionCreateDto
    {
        public int ChatId { get; set; }
        public string Content { get; set; }
    }

    public class QuestionResponseDto
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public string Content { get; set; }
        public string? Answer { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
