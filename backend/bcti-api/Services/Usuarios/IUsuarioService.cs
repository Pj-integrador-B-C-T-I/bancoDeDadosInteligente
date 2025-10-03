using BancoDeConhecimentoInteligenteAPI.Dtos.Usuarios;

namespace BancoDeConhecimentoInteligenteAPI.Services
{
    public interface IUsuarioService
    {
        Task<IEnumerable<ReadUsuarioDto>> GetAllAsync();
        Task<ReadUsuarioDto?> GetByIdAsync(int id);
        Task<ReadUsuarioDto?> UpdateAsync(int id, UpdateUsuarioDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
