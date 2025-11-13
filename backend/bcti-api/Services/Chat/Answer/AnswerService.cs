using BancoDeConhecimentoInteligenteAPI.Data;
using BancoDeConhecimentoInteligenteAPI.Dtos.Answer;
using BancoDeConhecimentoInteligenteAPI.Models;
using Microsoft.EntityFrameworkCore;

public class AnswerService : IAnswerService
{
    private readonly AppDbContext _context;

    public AnswerService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ReadAnswerDto> CreateAnswerAsync(CreateAnswerDto dto)
    {
        var answer = new Answer
        {
            QuestionId = dto.QuestionId,
            Content = dto.Content
        };

        _context.Answers.Add(answer);
        await _context.SaveChangesAsync();

        return new ReadAnswerDto
        {
            Id = answer.Id,
            QuestionId = answer.QuestionId,
            Content = answer.Content,
            CreatedAt = answer.CreatedAt
        };
    }

    public async Task<ReadAnswerDto?> GetByIdAsync(int id)
    {
        var answer = await _context.Answers.FindAsync(id);
        if (answer == null) return null;

        return new ReadAnswerDto
        {
            Id = answer.Id,
            QuestionId = answer.QuestionId,
            Content = answer.Content,
            CreatedAt = answer.CreatedAt
        };
    }

    public async Task<IEnumerable<ReadAnswerDto>> GetByQuestionIdAsync(int questionId)
    {
        return await _context.Answers
            .Where(a => a.QuestionId == questionId)
            .Select(a => new ReadAnswerDto
            {
                Id = a.Id,
                QuestionId = a.QuestionId,
                Content = a.Content,
                CreatedAt = a.CreatedAt
            })
            .ToListAsync();
    }

    public async Task<ReadAnswerDto?> UpdateAnswerByQuestionIdAsync(int questionId, string newContent)
    {
        // Busca a resposta pelo QuestionId
        var answer = await _context.Answers
            .FirstOrDefaultAsync(a => a.QuestionId == questionId);

        if (answer == null) return null;

        answer.Content = newContent;
        await _context.SaveChangesAsync();

        return new ReadAnswerDto
        {
            Id = answer.Id,
            QuestionId = answer.QuestionId,
            Content = answer.Content,
            CreatedAt = answer.CreatedAt
        };
    }


    public async Task<bool> RemoveAnswerAsync(int id)
    {
        var answer = await _context.Answers.FindAsync(id);
        if (answer == null) return false;

        _context.Answers.Remove(answer);
        await _context.SaveChangesAsync();
        return true;
    }
}
