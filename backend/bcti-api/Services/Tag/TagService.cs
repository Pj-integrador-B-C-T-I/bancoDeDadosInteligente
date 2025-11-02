using BancoDeConhecimentoInteligenteAPI.Data;
using BancoDeConhecimentoInteligenteAPI.Dtos;
using BancoDeConhecimentoInteligenteAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BancoDeConhecimentoInteligenteAPI.Services
{
    public class TagService : ITagService
    {
        private readonly AppDbContext _context;

        public TagService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TagDto>> GetAllAsync()
        {
            return await _context.Tags
                .Select(t => new TagDto { Id = t.Id, Name = t.Name })
                .ToListAsync();
        }

        public async Task<TagDto?> GetByIdAsync(int id)
        {
            var tag = await _context.Tags.FindAsync(id);
            if (tag == null) return null;

            return new TagDto { Id = tag.Id, Name = tag.Name };
        }

        public async Task<TagDto> CreateAsync(TagCreateDto dto)
        {
            var tag = new Tag { Name = dto.Name };
            _context.Tags.Add(tag);
            await _context.SaveChangesAsync();

            return new TagDto { Id = tag.Id, Name = tag.Name };
        }

        public async Task<TagDto?> UpdateAsync(int id, TagCreateDto dto)
        {
            var tag = await _context.Tags.FindAsync(id);
            if (tag == null) return null;

            tag.Name = dto.Name;
            await _context.SaveChangesAsync();

            return new TagDto { Id = tag.Id, Name = tag.Name };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var tag = await _context.Tags.FindAsync(id);
            if (tag == null) return false;

            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TagDto>> GetTagsByArticleIdAsync(int articleId)
        {
            return await _context.ArticleTags
                .Where(at => at.ArticleId == articleId)
                .Include(at => at.Tag)
                .Select(at => new TagDto
                {
                    Id = at.Tag.Id,
                    Name = at.Tag.Name
                })
                .ToListAsync();
        }

        public async Task<bool> AddTagToArticleAsync(int articleId, int tagId)
        {
            bool exists = await _context.ArticleTags.AnyAsync(at => at.ArticleId == articleId && at.TagId == tagId);
            if (exists) return false;

            _context.ArticleTags.Add(new ArticleTag { ArticleId = articleId, TagId = tagId });
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveTagFromArticleAsync(int articleId, int tagId)
        {
            var relation = await _context.ArticleTags.FirstOrDefaultAsync(at => at.ArticleId == articleId && at.TagId == tagId);
            if (relation == null) return false;

            _context.ArticleTags.Remove(relation);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
