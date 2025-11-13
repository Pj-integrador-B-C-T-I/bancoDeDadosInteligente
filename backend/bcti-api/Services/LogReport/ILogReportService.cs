using BancoDeConhecimentoInteligenteAPI.DTOs;
using BancoDeConhecimentoInteligenteAPI.Models;

namespace BancoDeConhecimentoInteligenteAPI.Services.Interfaces
{
    public interface ILogReportService
    {
        Task<IEnumerable<LogReportDTO>> GetAllAsync();
        Task<LogReportDTO?> GetByIdAsync(int id);
        Task<LogReportDTO> CreateAsync(CreateLogReportDTO dto);
        Task<bool> UpdateAsync(int id, UpdateLogReportDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
