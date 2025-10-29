using BancoDeConhecimentoInteligenteAPI.Data;
using BancoDeConhecimentoInteligenteAPI.Dtos.ChatMessage;
using BancoDeConhecimentoInteligenteAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BancoDeConhecimentoInteligenteAPI.Services
{
    public class ChatMessageService : IChatMessageService
    {
        private readonly AppDbContext _context;

        public ChatMessageService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ReadChatMessageDto> CreateAsync(CreateChatMessageDto dto)
        {
            var message = new ChatMessage
            {
                ChatId = dto.ChatId,
                Question = dto.Question,
                Answer = dto.Answer
            };

            _context.ChatMessages.Add(message);
            await _context.SaveChangesAsync();

            return new ReadChatMessageDto
            {
                Id = message.Id,
                ChatId = message.ChatId,
                Question = message.Question,
                Answer = message.Answer,
                CreatedAt = message.SentAt
            };
        }

        public async Task<IEnumerable<ReadChatMessageDto>> GetByChatIdAsync(int chatId)
        {
            return await _context.ChatMessages
                .Where(m => m.ChatId == chatId)
                .Select(m => new ReadChatMessageDto
                {
                    Id = m.Id,
                    ChatId = m.ChatId,
                    Question = m.Question,
                    Answer = m.Answer,
                    CreatedAt = m.SentAt
                })
                .ToListAsync();
        }

        public async Task<ReadChatMessageDto?> UpdateMessageAsync(int messageId, string newAnswer)
        {
            var msg = await _context.ChatMessages.FindAsync(messageId);
            if (msg == null) return null;

            msg.Answer = newAnswer;
            await _context.SaveChangesAsync();

            return new ReadChatMessageDto
            {
                Id = msg.Id,
                ChatId = msg.ChatId,
                Question = msg.Question,
                Answer = msg.Answer,
                CreatedAt = msg.SentAt
            };
        }

        public async Task<ReadChatMessageDto?> GetByIdAsync(int id)
        {
            var msg = await _context.ChatMessages.FindAsync(id);
            if (msg == null) return null;

            return new ReadChatMessageDto
            {
                Id = msg.Id,
                ChatId = msg.ChatId,
                Question = msg.Question,
                Answer = msg.Answer,
                CreatedAt = msg.SentAt
            };
        }

        public async Task<bool> RemoveMessageAsync(int id)
        {
            var msg = await _context.ChatMessages.FindAsync(id);
            if (msg == null) return false;

            _context.ChatMessages.Remove(msg);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
