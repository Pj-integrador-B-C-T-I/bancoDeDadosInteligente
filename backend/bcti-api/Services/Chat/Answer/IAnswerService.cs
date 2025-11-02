using BancoDeConhecimentoInteligenteAPI.Dtos.Answer;

public interface IAnswerService
{
    Task<ReadAnswerDto> CreateAnswerAsync(CreateAnswerDto dto);
    Task<ReadAnswerDto?> GetByIdAsync(int id);
    Task<IEnumerable<ReadAnswerDto>> GetByQuestionIdAsync(int questionId);
    Task<bool> RemoveAnswerAsync(int id);
    Task<ReadAnswerDto?> UpdateAnswerByQuestionIdAsync(int questionId, string newContent);
}
