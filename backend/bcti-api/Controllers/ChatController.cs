using BancoDeConhecimentoInteligenteAPI.Dtos.Chat;
using BancoDeConhecimentoInteligenteAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BancoDeConhecimentoInteligenteAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost]
        public async Task<ActionResult<ReadChatDto>> Create([FromBody] CreateChatDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var chat = await _chatService.CreateChatAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = chat.Id }, chat);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadChatDto>>> GetAll()
        {
            return Ok(await _chatService.GetAllChatsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReadChatDto>> GetById(int id)
        {
            var chat = await _chatService.GetByIdAsync(id);
            if (chat == null) return NotFound();
            return Ok(chat);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<ReadChatDto>>> GetByUser(int userId)
        {
            var chats = await _chatService.GetChatsByUserAsync(userId);
            if (!chats.Any()) return NotFound("Nenhum chat encontrado para esse usuário.");
            return Ok(chats);
        }

        [HttpPatch("{id}/title")]
        public async Task<ActionResult<ReadChatDto>> UpdateTitle(int id, [FromBody] UpdateChatDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
                return BadRequest("O título não pode ser vazio.");

            var updatedChat = await _chatService.UpdateChatTitleAsync(id, dto.Title);
            if (updatedChat == null) return NotFound("Chat não encontrado.");

            return Ok(updatedChat);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var removed = await _chatService.RemoveChatAsync(id);
            if (!removed) return NotFound();
            return NoContent();
        }

        [HttpGet("{id}/history")]
        public async Task<ActionResult<ChatHistoryDto>> GetChatHistory(int id)
        {
            var chatHistory = await _chatService.GetChatHistoryForClientAsync(id);
            if (chatHistory == null)
                return NotFound("Chat não encontrado.");

            return Ok(chatHistory);
        }
    }
}
