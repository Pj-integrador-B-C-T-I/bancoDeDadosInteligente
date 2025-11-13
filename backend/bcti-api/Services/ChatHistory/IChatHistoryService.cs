using System.Collections.Generic;
using System.Threading.Tasks;
using BancoDeConhecimentoInteligenteAPI.Dtos.ChatHistory;

namespace BancoDeConhecimentoInteligenteAPI.Services
{
    public interface IChatHistoryService
    {
        Task<ReadChatHistoryDto> CreateAsync(CreateChatHistoryDto dto);
        Task<IEnumerable<ReadChatHistoryDto>> GetAllAsync();
        Task<IEnumerable<ReadChatHistoryDto>> GetByUserIdAsync(int UserId);
        Task<ReadChatHistoryDto> GetByIdAsync(int id);
        Task<bool> RemoveByIdAsync(int id);
        Task<ReadChatHistoryDto?> UpdateAnswerAsync(int id, string newAnswer);

    }
}
