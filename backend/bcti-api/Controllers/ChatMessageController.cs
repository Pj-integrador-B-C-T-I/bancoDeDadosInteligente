using BancoDeConhecimentoInteligenteAPI.Dtos.ChatMessage;
using BancoDeConhecimentoInteligenteAPI.Models;
using BancoDeConhecimentoInteligenteAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BancoDeConhecimentoInteligenteAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatMessageController : ControllerBase
    {
        private readonly IChatMessageService _messageService;

        public ChatMessageController(IChatMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost]
        public async Task<ActionResult<ReadChatMessageDto>> Create([FromBody] CreateChatMessageDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var message = await _messageService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = message.Id }, message);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReadChatMessageDto>> GetById(int id)
        {
            var message = await _messageService.GetByIdAsync(id);
            if (message == null) return NotFound();
            return Ok(message);
        }

        [HttpGet("chat/{chatId}")]
        public async Task<ActionResult<IEnumerable<ReadChatMessageDto>>> GetByChatId(int chatId)
        {
            var messages = await _messageService.GetByChatIdAsync(chatId);
            if (!messages.Any()) return NotFound("Nenhuma mensagem encontrada para esse chat.");
            return Ok(messages);
        }

        [HttpPatch("{id}/message")]
        public async Task<ActionResult<ReadChatMessageDto>> UpdateMessage(int id, [FromBody] UpdateChatMessageDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Message)) return BadRequest("A mensagem não pode ser vazia.");

            var updatedMessage = await _messageService.UpdateMessageAsync(id, dto.Message);
            if (updatedMessage == null) return NotFound("Mensagem não encontrada.");

            return Ok(updatedMessage);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var removed = await _messageService.RemoveMessageAsync(id);
            if (!removed) return NotFound();
            return NoContent();
        }
    }
}
