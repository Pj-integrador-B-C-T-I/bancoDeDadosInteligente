using BancoDeConhecimentoInteligenteAPI.Dtos.Question;

namespace BancoDeConhecimentoInteligenteAPI.Services
{
    public interface IQuestionService
    {
        Task<QuestionResponseDto> CreateQuestionAsync(QuestionCreateDto dto);
        Task<IEnumerable<QuestionResponseDto>> GetChatHistoryAsync(int chatId);
    }
}
