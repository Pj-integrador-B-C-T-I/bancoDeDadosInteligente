using System;
using System.Collections.Generic;

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

        // Opcional: listar perguntas do chat
        public List<string>? Questions { get; set; }
    }

    public class UpdateChatDto
    {
        public string Title { get; set; }
    }


    public class ChatHistoryDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<MessageDto> Messages { get; set; }
    }

    public class MessageDto
    {
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
