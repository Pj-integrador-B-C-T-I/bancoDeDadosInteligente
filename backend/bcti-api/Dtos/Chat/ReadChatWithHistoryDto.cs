using System;
using System.Collections.Generic;

namespace BancoDeConhecimentoInteligenteAPI.Dtos.Chat
{
    public class ReadChatWithHistoryDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<QuestionHistoryDto> Questions { get; set; } = new();
    }

    public class QuestionHistoryDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        public AnswerHistoryDto Answer { get; set; }
    }

    public class AnswerHistoryDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
