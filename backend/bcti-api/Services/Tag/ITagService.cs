using BancoDeConhecimentoInteligenteAPI.Dtos;

namespace BancoDeConhecimentoInteligenteAPI.Services
{
    public interface ITagService
    {
        Task<IEnumerable<TagDto>> GetAllAsync();
        Task<TagDto?> GetByIdAsync(int id);
        Task<TagDto> CreateAsync(TagCreateDto dto);
        Task<TagDto?> UpdateAsync(int id, TagCreateDto dto);
        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<TagDto>> GetTagsByArticleIdAsync(int articleId);
        Task<bool> AddTagToArticleAsync(int articleId, int tagId);
        Task<bool> RemoveTagFromArticleAsync(int articleId, int tagId);
    }
}
