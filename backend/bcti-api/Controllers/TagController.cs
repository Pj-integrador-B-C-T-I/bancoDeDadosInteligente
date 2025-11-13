using BancoDeConhecimentoInteligenteAPI.Dtos;
using BancoDeConhecimentoInteligenteAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BancoDeConhecimentoInteligenteAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagController : ControllerBase
    {
        private readonly ITagService _service;

        public TagController(ITagService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tags = await _service.GetAllAsync();
            return Ok(tags);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var tag = await _service.GetByIdAsync(id);
            if (tag == null) return NotFound();
            return Ok(tag);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TagCreateDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TagCreateDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        [HttpGet("article/{articleId}")]
        public async Task<IActionResult> GetTagsByArticleId(int articleId)
        {
            var tags = await _service.GetTagsByArticleIdAsync(articleId);
            return Ok(tags);
        }

        [HttpPost("article/{articleId}/tag/{tagId}")]
        public async Task<IActionResult> AddTagToArticle(int articleId, int tagId)
        {
            var added = await _service.AddTagToArticleAsync(articleId, tagId);
            if (!added) return BadRequest("Tag já associada a este artigo.");
            return Ok();
        }

        [HttpDelete("article/{articleId}/tag/{tagId}")]
        public async Task<IActionResult> RemoveTagFromArticle(int articleId, int tagId)
        {
            var removed = await _service.RemoveTagFromArticleAsync(articleId, tagId);
            if (!removed) return NotFound("Relação não encontrada.");
            return NoContent();
        }
    }
}
