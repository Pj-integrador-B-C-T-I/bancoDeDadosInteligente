using BancoDeConhecimentoInteligenteAPI.Dtos.Chat;

public interface IChatService
{
    Task<ReadChatDto> CreateChatAsync(CreateChatDto dto);
    Task<IEnumerable<ReadChatDto>> GetChatsByUserAsync(int userId);
    Task<ReadChatDto?> UpdateChatTitleAsync(int chatId, string newTitle); // novo método
    Task<ReadChatDto?> GetByIdAsync(int id);
    Task<IEnumerable<ReadChatDto>> GetAllChatsAsync();
    Task<bool> RemoveChatAsync(int id);

}
