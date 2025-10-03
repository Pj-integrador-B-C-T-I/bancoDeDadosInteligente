using BancoDeConhecimentoInteligenteAPI.Dtos.Usuarios;
using BancoDeConhecimentoInteligenteAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BancoDeConhecimentoInteligenteAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // GET: api/usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadUsuarioDto>>> GetAll()
        {
            var usuarios = await _usuarioService.GetAllAsync();
            return Ok(usuarios);
        }

        // GET: api/usuarios/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadUsuarioDto>> GetById(int id)
        {
            var usuario = await _usuarioService.GetByIdAsync(id);
            if (usuario == null) return NotFound();
            return Ok(usuario);
        }

        // PUT: api/usuarios/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<ReadUsuarioDto>> Update(int id, UpdateUsuarioDto dto)
        {
            var usuario = await _usuarioService.UpdateAsync(id, dto);
            if (usuario == null) return NotFound();
            return Ok(usuario);
        }

        // DELETE: api/usuarios/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _usuarioService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}