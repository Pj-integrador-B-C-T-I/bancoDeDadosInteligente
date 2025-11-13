using BancoDeConhecimentoInteligenteAPI.Data;
using BancoDeConhecimentoInteligenteAPI.DTOs;
using BancoDeConhecimentoInteligenteAPI.Models;
using BancoDeConhecimentoInteligenteAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BancoDeConhecimentoInteligenteAPI.Services
{
    public class LogReportService : ILogReportService
    {
        private readonly AppDbContext _context;

        public LogReportService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LogReportDTO>> GetAllAsync()
        {
            return await _context.Set<LogReport>()
                .Include(l => l.Author)
                .Select(l => new LogReportDTO
                {
                    Id = l.Id,
                    Title = l.Title,
                    Description = l.Description,
                    SystemAffected = l.SystemAffected,
                    ErrorType = l.ErrorType,
                    Resolution = l.Resolution,
                    AuthorId = l.AuthorId,
                    AuthorName = l.Author != null ? l.Author.Nome : null,
                    CreatedAt = l.CreatedAt,
                    UpdatedAt = l.UpdatedAt
                })
                .ToListAsync();
        }

        public async Task<LogReportDTO?> GetByIdAsync(int id)
        {
            var log = await _context.Set<LogReport>()
                .Include(l => l.Author)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (log == null) return null;

            return new LogReportDTO
            {
                Id = log.Id,
                Title = log.Title,
                Description = log.Description,
                SystemAffected = log.SystemAffected,
                ErrorType = log.ErrorType,
                Resolution = log.Resolution,
                AuthorId = log.AuthorId,
                AuthorName = log.Author?.Nome,
                CreatedAt = log.CreatedAt,
                UpdatedAt = log.UpdatedAt
            };
        }

        public async Task<LogReportDTO> CreateAsync(CreateLogReportDTO dto)
        {
            var entity = new LogReport
            {
                Title = dto.Title,
                Description = dto.Description,
                SystemAffected = dto.SystemAffected,
                ErrorType = dto.ErrorType,
                Resolution = dto.Resolution,
                AuthorId = dto.AuthorId
            };

            _context.Add(entity);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(entity.Id) ?? throw new Exception("Erro ao criar LogReport");
        }

        public async Task<bool> UpdateAsync(int id, UpdateLogReportDTO dto)
        {
            var log = await _context.Set<LogReport>().FindAsync(id);
            if (log == null) return false;

            if (!string.IsNullOrEmpty(dto.Title)) log.Title = dto.Title;
            if (!string.IsNullOrEmpty(dto.Description)) log.Description = dto.Description;
            if (!string.IsNullOrEmpty(dto.SystemAffected)) log.SystemAffected = dto.SystemAffected;
            if (!string.IsNullOrEmpty(dto.ErrorType)) log.ErrorType = dto.ErrorType;
            if (!string.IsNullOrEmpty(dto.Resolution)) log.Resolution = dto.Resolution;

            log.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var log = await _context.Set<LogReport>().FindAsync(id);
            if (log == null) return false;

            _context.Remove(log);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
