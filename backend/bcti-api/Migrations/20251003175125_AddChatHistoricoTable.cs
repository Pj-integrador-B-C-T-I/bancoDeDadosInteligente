using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BancoDeConhecimentoInteligente.Migrations
{
    /// <inheritdoc />
    public partial class AddChatHistoricoTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatHistoricos_Usuarios_UsuarioId",
                table: "ChatHistoricos");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "ChatHistoricos",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Resposta",
                table: "ChatHistoricos",
                newName: "Question");

            migrationBuilder.RenameColumn(
                name: "Pergunta",
                table: "ChatHistoricos",
                newName: "Answer");

            migrationBuilder.RenameColumn(
                name: "CriadoEm",
                table: "ChatHistoricos",
                newName: "CreateAt");

            migrationBuilder.RenameIndex(
                name: "IX_ChatHistoricos_UsuarioId",
                table: "ChatHistoricos",
                newName: "IX_ChatHistoricos_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatHistoricos_Usuarios_UserId",
                table: "ChatHistoricos",
                column: "UserId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatHistoricos_Usuarios_UserId",
                table: "ChatHistoricos");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ChatHistoricos",
                newName: "UsuarioId");

            migrationBuilder.RenameColumn(
                name: "Question",
                table: "ChatHistoricos",
                newName: "Resposta");

            migrationBuilder.RenameColumn(
                name: "CreateAt",
                table: "ChatHistoricos",
                newName: "CriadoEm");

            migrationBuilder.RenameColumn(
                name: "Answer",
                table: "ChatHistoricos",
                newName: "Pergunta");

            migrationBuilder.RenameIndex(
                name: "IX_ChatHistoricos_UserId",
                table: "ChatHistoricos",
                newName: "IX_ChatHistoricos_UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatHistoricos_Usuarios_UsuarioId",
                table: "ChatHistoricos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
