using Microsoft.EntityFrameworkCore;
using BancoDeConhecimentoInteligenteAPI.Models;

namespace BancoDeConhecimentoInteligenteAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<ChatHistory> ChatHistories { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Article> Articles { get; set; }

    }
}