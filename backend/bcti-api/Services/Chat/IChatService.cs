using BancoDeConhecimentoInteligenteAPI.Dtos.Chat;

namespace BancoDeConhecimentoInteligenteAPI.Services
{
    public interface IChatService
    {
        Task<ReadChatDto> CreateChatAsync(CreateChatDto dto);
        Task<IEnumerable<ReadChatDto>> GetChatsByUserAsync(int userId);
        Task<ReadChatDto?> UpdateChatTitleAsync(int chatId, string newTitle);
        Task<ReadChatDto?> GetByIdAsync(int id);
        Task<IEnumerable<ReadChatDto>> GetAllChatsAsync();
        Task<bool> RemoveChatAsync(int id);
        Task<ChatHistoryDto?> GetChatHistoryForClientAsync(int chatId);
    }
}
