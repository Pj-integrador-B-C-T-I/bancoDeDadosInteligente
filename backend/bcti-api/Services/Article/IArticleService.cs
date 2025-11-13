using BancoDeConhecimentoInteligenteAPI.Dtos;

namespace BancoDeConhecimentoInteligenteAPI.Services
{
    public interface IArticleService
    {
        Task<IEnumerable<ArticleDto>> GetAllAsync();
        Task<ArticleDto?> GetByIdAsync(int id);
        Task<ArticleDto> CreateAsync(ArticleCreateDto dto);
        Task<ArticleDto?> UpdateAsync(int id, ArticleCreateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
