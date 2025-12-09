using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BancoDeConhecimentoInteligenteAPI.Data;
using BancoDeConhecimentoInteligenteAPI.Models;
using BancoDeConhecimentoInteligenteAPI.Dtos.Question;

namespace BancoDeConhecimentoInteligenteAPI.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly AppDbContext _context;

        public QuestionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<QuestionResponseDto> CreateQuestionAsync(QuestionCreateDto dto)
        {
            var question = new Question
            {
                ChatId = dto.ChatId,
                Content = dto.Content
            };

            _context.Questions.Add(question);
            await _context.SaveChangesAsync();

            // Placeholder - futuramente poderá vir da IA
            var answer = new Answer
            {
                QuestionId = question.Id,
                Content = "Resposta gerada automaticamente"
            };

            _context.Answers.Add(answer);
            await _context.SaveChangesAsync();

            return new QuestionResponseDto
            {
                Id = question.Id,
                ChatId = question.ChatId,
                Content = question.Content,
                Answer = answer.Content,
                CreatedAt = question.CreatedAt
            };
        }

        public async Task<IEnumerable<QuestionResponseDto>> GetChatHistoryAsync(int chatId)
        {
            return await _context.Questions
                .Include(q => q.Answer)
                .Where(q => q.ChatId == chatId)
                .OrderBy(q => q.CreatedAt)
                .Select(q => new QuestionResponseDto
                {
                    Id = q.Id,
                    ChatId = q.ChatId,
                    Content = q.Content,
                    Answer = q.Answer != null ? q.Answer.Content : null,
                    CreatedAt = q.CreatedAt
                })
                .ToListAsync();
        }
    }
}
