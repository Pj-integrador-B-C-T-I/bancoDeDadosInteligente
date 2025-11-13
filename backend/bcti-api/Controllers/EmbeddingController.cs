using BancoDeConhecimentoInteligenteAPI.Dtos;
using BancoDeConhecimentoInteligenteAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BancoDeConhecimentoInteligenteAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmbeddingController : ControllerBase
    {
        private readonly IEmbeddingService _embeddingService;

        public EmbeddingController(IEmbeddingService embeddingService)
        {
            _embeddingService = embeddingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var embeddings = await _embeddingService.GetAllAsync();
            return Ok(embeddings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var embedding = await _embeddingService.GetByIdAsync(id);
            if (embedding == null) return NotFound();
            return Ok(embedding);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmbeddingCreateDto dto)
        {
            var created = await _embeddingService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _embeddingService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
