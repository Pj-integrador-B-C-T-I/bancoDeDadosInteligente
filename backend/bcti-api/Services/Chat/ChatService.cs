using BancoDeConhecimentoInteligenteAPI.Data;
using BancoDeConhecimentoInteligenteAPI.Dtos.Chat;
using BancoDeConhecimentoInteligenteAPI.Models;
using Microsoft.EntityFrameworkCore;

public class ChatService : IChatService
{
    private readonly AppDbContext _context;

    public ChatService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ReadChatDto> CreateChatAsync(CreateChatDto dto)
    {
        var chat = new Chat
        {
            UserId = dto.UserId,
            Title = dto.Title
        };

        _context.Chats.Add(chat);
        await _context.SaveChangesAsync();

        return new ReadChatDto
        {
            Id = chat.Id,
            UserId = chat.UserId,
            Title = chat.Title,
            CreatedAt = chat.CreatedAt
        };
    }

    public async Task<IEnumerable<ReadChatDto>> GetChatsByUserAsync(int userId)
    {
        return await _context.Chats
            .Where(c => c.UserId == userId)
            .Select(c => new ReadChatDto
            {
                Id = c.Id,
                UserId = c.UserId,
                Title = c.Title,
                CreatedAt = c.CreatedAt
            })
            .ToListAsync();
    }

    public async Task<ReadChatDto?> UpdateChatTitleAsync(int chatId, string newTitle)
    {
        var chat = await _context.Chats.FindAsync(chatId);
        if (chat == null) return null;

        chat.Title = newTitle;
        await _context.SaveChangesAsync();

        return new ReadChatDto
        {
            Id = chat.Id,
            UserId = chat.UserId,
            Title = chat.Title,
            CreatedAt = chat.CreatedAt
        };
    }


        public async Task<ReadChatDto?> GetByIdAsync(int id)
    {
        return await _context.Chats
            .Where(c => c.Id == id)
            .Select(c => new ReadChatDto
            {
                Id = c.Id,
                UserId = c.UserId,
                Title = c.Title,
                CreatedAt = c.CreatedAt
            })
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<ReadChatDto>> GetAllChatsAsync()
    {
        return await _context.Chats
            .Select(c => new ReadChatDto
            {
                Id = c.Id,
                UserId = c.UserId,
                Title = c.Title,
                CreatedAt = c.CreatedAt
            })
            .ToListAsync();
    }

    public async Task<bool> RemoveChatAsync(int id)
    {
        var chat = await _context.Chats.FindAsync(id);
        if (chat == null) return false;

        _context.Chats.Remove(chat);
        await _context.SaveChangesAsync();
        return true;
    }


}
