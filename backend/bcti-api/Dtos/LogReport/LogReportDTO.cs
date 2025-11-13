namespace BancoDeConhecimentoInteligenteAPI.DTOs
{
    public class LogReportDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string SystemAffected { get; set; }
        public string ErrorType { get; set; }
        public string Resolution { get; set; }
        public int? AuthorId { get; set; }
        public string? AuthorName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class CreateLogReportDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string SystemAffected { get; set; }
        public string ErrorType { get; set; }
        public string Resolution { get; set; }
        public int? AuthorId { get; set; }
    }

    public class UpdateLogReportDTO
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? SystemAffected { get; set; }
        public string? ErrorType { get; set; }
        public string? Resolution { get; set; }
    }
}
