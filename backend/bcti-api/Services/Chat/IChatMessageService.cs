using BancoDeConhecimentoInteligenteAPI.Dtos.ChatMessage;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BancoDeConhecimentoInteligenteAPI.Services
{

    public interface IChatMessageService
    {
        Task<ReadChatMessageDto> CreateAsync(CreateChatMessageDto dto);
        Task<IEnumerable<ReadChatMessageDto>> GetByChatIdAsync(int chatId);
        Task<ReadChatMessageDto?> UpdateMessageAsync(int messageId, string newAnswer);
        Task<ReadChatMessageDto?> GetByIdAsync(int id);
        Task<bool> RemoveMessageAsync(int id);
    }
}
