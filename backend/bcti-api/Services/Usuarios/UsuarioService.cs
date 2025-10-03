using BancoDeConhecimentoInteligenteAPI.Data;
using BancoDeConhecimentoInteligenteAPI.Dtos.Usuarios;
using BancoDeConhecimentoInteligenteAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BancoDeConhecimentoInteligenteAPI.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly AppDbContext _context;

        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ReadUsuarioDto>> GetAllAsync()
        {
            return await _context.Usuarios
                .Select(u => new ReadUsuarioDto
                {
                    Id = u.Id,
                    Nome = u.Nome,
                    Email = u.Email,
                    Telefone = u.Telefone,
                    Cpf = u.Cpf,
                    CriadoEm = u.CriadoEm,
                    Tipo = u.Tipo,
                    Ativo = u.Ativo,
                    EmailVerificado = u.EmailVerificado
                })
                .ToListAsync();
        }

        public async Task<ReadUsuarioDto?> GetByIdAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return null;

            return new ReadUsuarioDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Telefone = usuario.Telefone,
                Cpf = usuario.Cpf,
                CriadoEm = usuario.CriadoEm,
                Tipo = usuario.Tipo,
                Ativo = usuario.Ativo,
                EmailVerificado = usuario.EmailVerificado
            };
        }

        public async Task<ReadUsuarioDto?> UpdateAsync(int id, UpdateUsuarioDto dto)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return null;

            usuario.Nome = dto.Nome;
            usuario.Telefone = dto.Telefone;
            usuario.Tipo = dto.Tipo;
            usuario.Ativo = dto.Ativo;

            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();

            return new ReadUsuarioDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Telefone = usuario.Telefone,
                Cpf = usuario.Cpf,
                CriadoEm = usuario.CriadoEm,
                Tipo = usuario.Tipo,
                Ativo = usuario.Ativo,
                EmailVerificado = usuario.EmailVerificado
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return false;

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
