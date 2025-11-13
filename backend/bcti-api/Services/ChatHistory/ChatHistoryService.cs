using BancoDeConhecimentoInteligenteAPI.Data;
using BancoDeConhecimentoInteligenteAPI.Dtos.ChatHistory;
using BancoDeConhecimentoInteligenteAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace BancoDeConhecimentoInteligenteAPI.Services
{

    public class ChatHistoryService : IChatHistoryService
    {
        private readonly AppDbContext _context;

        public ChatHistoryService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<ReadChatHistoryDto> CreateAsync(CreateChatHistoryDto dto)
        {
            var chat = new ChatHistory
            {
                UserId = dto.UserId,
                Answer = dto.Answer,
                Question = dto.Question,
                CreateAt = DateTime.UtcNow
            };

            _context.ChatHistories.Add(chat);
            await _context.SaveChangesAsync();

            return new ReadChatHistoryDto
            {
                Id = chat.Id,
                UserId = chat.UserId,
                Answer = chat.Answer,
                Question = chat.Question,
                CreateAt = chat.CreateAt
            };
        }

        public async Task<IEnumerable<ReadChatHistoryDto>> GetAllAsync()
        {
            return await _context.ChatHistories
                .Select(c => new ReadChatHistoryDto
                {
                    Id = c.Id,
                    UserId = c.UserId,
                    Question = c.Question,
                    Answer = c.Answer,
                    CreateAt = c.CreateAt
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<ReadChatHistoryDto>> GetByUserIdAsync(int UserId)
        {
            return await _context.ChatHistories
                .Where(c => c.UserId == UserId)
                .Select(c => new ReadChatHistoryDto
                {
                    Id = c.Id,
                    UserId = c.UserId,
                    Question = c.Question,
                    Answer = c.Answer,
                    CreateAt = c.CreateAt
                })
                .ToListAsync();
        }

        public async Task<ReadChatHistoryDto> GetByIdAsync(int id)
        {
            var chat = await _context.ChatHistories.FindAsync(id);
            if (chat == null) return null;

            return new ReadChatHistoryDto
            {
                Id = chat.Id,
                UserId = chat.UserId,
                Question = chat.Question,
                Answer = chat.Answer,
                CreateAt = chat.CreateAt
            };
        }

        public async Task<bool> RemoveByIdAsync(int id)
        {
            var chat = await _context.ChatHistories.FindAsync(id);
            if (chat == null) return false;

            _context.ChatHistories.Remove(chat);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ReadChatHistoryDto?> UpdateAnswerAsync(int id, string newAnswer)
        {
            var chat = await _context.ChatHistories.FindAsync(id);
            if (chat == null) return null;

            chat.Answer = newAnswer;
            await _context.SaveChangesAsync();

            return new ReadChatHistoryDto
            {
                Id = chat.Id,
                UserId = chat.UserId,
                Question = chat.Question,
                Answer = chat.Answer,
                CreateAt = chat.CreateAt
            };
        }

    }
}
