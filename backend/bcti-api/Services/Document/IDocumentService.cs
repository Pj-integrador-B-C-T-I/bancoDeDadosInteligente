using BancoDeConhecimentoInteligenteAPI.Dtos;

namespace BancoDeConhecimentoInteligenteAPI.Services
{
    public interface IDocumentService
    {
        Task<IEnumerable<DocumentDto>> GetAllAsync();
        Task<DocumentDto?> GetByIdAsync(int id);
        Task<DocumentDto> CreateAsync(DocumentCreateDto dto);
        Task<DocumentDto?> UpdateAsync(int id, DocumentCreateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
