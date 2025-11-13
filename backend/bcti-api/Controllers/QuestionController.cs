using BancoDeConhecimentoInteligenteAPI.Dtos.Question;
using BancoDeConhecimentoInteligenteAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BancoDeConhecimentoInteligenteAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        /// <summary>
        /// Cria uma nova pergunta e gera uma resposta automática (temporária)
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<QuestionResponseDto>> Create([FromBody] QuestionCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _questionService.CreateQuestionAsync(dto);
            return CreatedAtAction(nameof(GetChatHistory), new { chatId = result.ChatId }, result);
        }

        /// <summary>
        /// Retorna o histórico completo de perguntas e respostas de um chat
        /// </summary>
        [HttpGet("chat/{chatId}")]
        public async Task<ActionResult<IEnumerable<QuestionResponseDto>>> GetChatHistory(int chatId)
        {
            var history = await _questionService.GetChatHistoryAsync(chatId);
            if (!history.Any()) return NotFound("Nenhuma pergunta encontrada para este chat.");
            return Ok(history);
        }
    }
}
