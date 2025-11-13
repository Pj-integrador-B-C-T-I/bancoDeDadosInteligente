using BancoDeConhecimentoInteligenteAPI.Data;
using BancoDeConhecimentoInteligenteAPI.Dtos;
using BancoDeConhecimentoInteligenteAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BancoDeConhecimentoInteligenteAPI.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly AppDbContext _context;

        public DocumentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DocumentDto>> GetAllAsync()
        {
            return await _context.Documents
                .Select(d => new DocumentDto
                {
                    Id = d.Id,
                    ArticleId = d.ArticleId,
                    FileName = d.FileName,
                    FilePath = d.FilePath,
                    FileType = d.FileType,
                    CreatedAt = d.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<DocumentDto?> GetByIdAsync(int id)
        {
            var doc = await _context.Documents.FindAsync(id);
            if (doc == null) return null;

            return new DocumentDto
            {
                Id = doc.Id,
                ArticleId = doc.ArticleId,
                FileName = doc.FileName,
                FilePath = doc.FilePath,
                FileType = doc.FileType,
                CreatedAt = doc.CreatedAt
            };
        }

        public async Task<DocumentDto> CreateAsync(DocumentCreateDto dto)
        {
            var doc = new Document
            {
                ArticleId = dto.ArticleId,
                FileName = dto.FileName,
                FilePath = dto.FilePath,
                FileType = dto.FileType,
                CreatedAt = DateTime.UtcNow
            };

            _context.Documents.Add(doc);
            await _context.SaveChangesAsync();

            return new DocumentDto
            {
                Id = doc.Id,
                ArticleId = doc.ArticleId,
                FileName = doc.FileName,
                FilePath = doc.FilePath,
                FileType = doc.FileType,
                CreatedAt = doc.CreatedAt
            };
        }

        public async Task<DocumentDto?> UpdateAsync(int id, DocumentCreateDto dto)
        {
            var doc = await _context.Documents.FindAsync(id);
            if (doc == null) return null;

            doc.ArticleId = dto.ArticleId;
            doc.FileName = dto.FileName;
            doc.FilePath = dto.FilePath;
            doc.FileType = dto.FileType;

            await _context.SaveChangesAsync();

            return new DocumentDto
            {
                Id = doc.Id,
                ArticleId = doc.ArticleId,
                FileName = doc.FileName,
                FilePath = doc.FilePath,
                FileType = doc.FileType,
                CreatedAt = doc.CreatedAt
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var doc = await _context.Documents.FindAsync(id);
            if (doc == null) return false;

            _context.Documents.Remove(doc);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
