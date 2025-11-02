using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BancoDeConhecimentoInteligente.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEmbeddingVectorToJson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Vector",
                table: "Embeddings",
                newName: "VectorJson");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VectorJson",
                table: "Embeddings",
                newName: "Vector");
        }
    }
}
