using BancoDeConhecimentoInteligenteAPI.Data;
using BancoDeConhecimentoInteligenteAPI.Dtos;
using BancoDeConhecimentoInteligenteAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BancoDeConhecimentoInteligenteAPI.Services
{
    public class EmbeddingService : IEmbeddingService
    {
        private readonly AppDbContext _context;

        public EmbeddingService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmbeddingDto>> GetAllAsync()
        {
            return await _context.Embeddings
                .Include(e => e.Article)
                .Select(e => new EmbeddingDto
                {
                    Id = e.Id,
                    ArticleId = e.ArticleId,
                    Chunk = e.Chunk,
                    Source = e.Source,
                    SourceId = e.SourceId,
                    CreatedAt = e.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<EmbeddingDto?> GetByIdAsync(int id)
        {
            var embedding = await _context.Embeddings
                .Include(e => e.Article)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (embedding == null) return null;

            return new EmbeddingDto
            {
                Id = embedding.Id,
                ArticleId = embedding.ArticleId,
                Chunk = embedding.Chunk,
                Source = embedding.Source,
                SourceId = embedding.SourceId,
                CreatedAt = embedding.CreatedAt
            };
        }

        public async Task<EmbeddingDto> CreateAsync(EmbeddingCreateDto dto)
        {
            var embedding = new Embedding
            {
                ArticleId = dto.ArticleId,
                Chunk = dto.Chunk,
                Source = dto.Source,
                SourceId = dto.SourceId,
                CreatedAt = DateTime.UtcNow
            };

            _context.Embeddings.Add(embedding);
            await _context.SaveChangesAsync();

            return new EmbeddingDto
            {
                Id = embedding.Id,
                ArticleId = embedding.ArticleId,
                Chunk = embedding.Chunk,
                Source = embedding.Source,
                SourceId = embedding.SourceId,
                CreatedAt = embedding.CreatedAt
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var embedding = await _context.Embeddings.FindAsync(id);
            if (embedding == null) return false;

            _context.Embeddings.Remove(embedding);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
