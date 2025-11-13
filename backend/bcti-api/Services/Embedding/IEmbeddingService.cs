using BancoDeConhecimentoInteligenteAPI.Dtos;

namespace BancoDeConhecimentoInteligenteAPI.Services
{
    public interface IEmbeddingService
    {
        Task<IEnumerable<EmbeddingDto>> GetAllAsync();
        Task<EmbeddingDto?> GetByIdAsync(int id);
        Task<EmbeddingDto> CreateAsync(EmbeddingCreateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
