using System;

namespace BancoDeConhecimentoInteligenteAPI.Dtos.Answer
{
    public class CreateAnswerDto
    {
        public int QuestionId { get; set; }
        public string Content { get; set; }
    }

    public class ReadAnswerDto
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class UpdateAnswerDto
    {
        public string Content { get; set; }
    }
}
