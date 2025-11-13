using BancoDeConhecimentoInteligenteAPI.Dtos.Answer;
using Microsoft.AspNetCore.Mvc;

namespace BancoDeConhecimentoInteligenteAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerService _answerService;

        public AnswerController(IAnswerService answerService)
        {
            _answerService = answerService;
        }

        [HttpPost]
        public async Task<ActionResult<ReadAnswerDto>> Create([FromBody] CreateAnswerDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var answer = await _answerService.CreateAnswerAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = answer.Id }, answer);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReadAnswerDto>> GetById(int id)
        {
            var answer = await _answerService.GetByIdAsync(id);
            if (answer == null) return NotFound();
            return Ok(answer);
        }

        [HttpGet("question/{questionId}")]
        public async Task<ActionResult<IEnumerable<ReadAnswerDto>>> GetByQuestion(int questionId)
        {
            var answers = await _answerService.GetByQuestionIdAsync(questionId);
            return Ok(answers);
        }

        [HttpPatch("by-question/{questionId}")]
        public async Task<ActionResult<ReadAnswerDto>> UpdateByQuestionId(int questionId, [FromBody] UpdateAnswerDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Content))
                return BadRequest("O conteúdo da resposta não pode ser vazio.");

            var updated = await _answerService.UpdateAnswerByQuestionIdAsync(questionId, dto.Content);
            if (updated == null) return NotFound();

            return Ok(updated);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var removed = await _answerService.RemoveAnswerAsync(id);
            if (!removed) return NotFound();
            return NoContent();
        }
    }
}
