using BancoDeConhecimentoInteligenteAPI.Dtos.ChatHistory;
using BancoDeConhecimentoInteligenteAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BancoDeConhecimentoInteligenteAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatHistoryController : ControllerBase
    {
        private readonly IChatHistoryService _chatHistoryService;

        public ChatHistoryController(IChatHistoryService chatHistoryService)
        {
            _chatHistoryService = chatHistoryService;
        }

        // POST: api/ChatHistory
        [HttpPost]
        public async Task<ActionResult<ReadChatHistoryDto>> Create([FromBody] CreateChatHistoryDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var chat = await _chatHistoryService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = chat.Id }, chat);
        }

        // GET: api/ChatHistory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadChatHistoryDto>>> GetAll()
        {
            var chats = await _chatHistoryService.GetAllAsync();
            return Ok(chats);
        }

        // GET: api/ChatHistory/user/5
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<ReadChatHistoryDto>>> GetByUserId(int userId)
        {
            var chats = await _chatHistoryService.GetByUserIdAsync(userId);
            if (!chats.Any()) return NotFound("Nenhum histórico encontrado para esse usuário.");
            return Ok(chats);
        }

        // GET: api/ChatHistory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadChatHistoryDto>> GetById(int id)
        {
            var chat = await _chatHistoryService.GetByIdAsync(id);
            if (chat == null) return NotFound();
            return Ok(chat);
        }

        // DELETE: api/ChatHistory/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveById(int id)
        {
            var removed = await _chatHistoryService.RemoveByIdAsync(id);
            if (!removed) return NotFound();
            return NoContent();
        }
    }
}
