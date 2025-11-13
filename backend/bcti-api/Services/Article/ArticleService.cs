using BancoDeConhecimentoInteligenteAPI.Data;
using BancoDeConhecimentoInteligenteAPI.Dtos;
using BancoDeConhecimentoInteligenteAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BancoDeConhecimentoInteligenteAPI.Services
{
    public class ArticleService : IArticleService
    {
        private readonly AppDbContext _context;

        public ArticleService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ArticleDto>> GetAllAsync()
        {
            return await _context.Articles
                .Include(a => a.Author) 
                .Select(a => new ArticleDto
                {
                    Id = a.Id,
                    Title = a.Title,
                    Description = a.Description,
                    Content = a.Content,
                    AuthorId = a.AuthorId,
                    AuthorName = a.Author != null ? a.Author.Nome : string.Empty,
                    CategoryId = a.CategoryId,
                    CreatedAt = a.CreatedAt,
                    UpdatedAt = a.UpdatedAt
                })
                .ToListAsync();
        }

        public async Task<ArticleDto?> GetByIdAsync(int id)
        {
            var article = await _context.Articles
                .Include(a => a.Author)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (article == null) return null;

            return new ArticleDto
            {
                Id = article.Id,
                Title = article.Title,
                Description = article.Description,
                Content = article.Content,
                AuthorId = article.AuthorId,
                AuthorName = article.Author?.Nome,
                CategoryId = article.CategoryId,
                CreatedAt = article.CreatedAt,
                UpdatedAt = article.UpdatedAt
            };
        }

        public async Task<ArticleDto> CreateAsync(ArticleCreateDto dto)
        {
            var article = new Article
            {
                Title = dto.Title,
                Description = dto.Description,
                Content = dto.Content,
                AuthorId = dto.AuthorId,
                CategoryId = dto.CategoryId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Articles.Add(article);
            await _context.SaveChangesAsync();

            // Recarrega com o Author incluso
            await _context.Entry(article).Reference(a => a.Author).LoadAsync();

            return new ArticleDto
            {
                Id = article.Id,
                Title = article.Title,
                Description = article.Description,
                Content = article.Content,
                AuthorId = article.AuthorId,
                AuthorName = article.Author?.Nome,
                CategoryId = article.CategoryId,
                CreatedAt = article.CreatedAt,
                UpdatedAt = article.UpdatedAt
            };
        }

        public async Task<ArticleDto?> UpdateAsync(int id, ArticleCreateDto dto)
        {
            var article = await _context.Articles.Include(a => a.Author).FirstOrDefaultAsync(a => a.Id == id);
            if (article == null) return null;

            article.Title = dto.Title;
            article.Description = dto.Description;
            article.Content = dto.Content;
            article.AuthorId = dto.AuthorId;
            article.CategoryId = dto.CategoryId;
            article.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new ArticleDto
            {
                Id = article.Id,
                Title = article.Title,
                Description = article.Description,
                Content = article.Content,
                AuthorId = article.AuthorId,
                AuthorName = article.Author?.Nome,
                CategoryId = article.CategoryId,
                CreatedAt = article.CreatedAt,
                UpdatedAt = article.UpdatedAt
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null) return false;

            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
