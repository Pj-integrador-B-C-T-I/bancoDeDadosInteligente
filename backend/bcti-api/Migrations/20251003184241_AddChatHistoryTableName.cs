using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BancoDeConhecimentoInteligente.Migrations
{
    /// <inheritdoc />
    public partial class AddChatHistoryTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatHistory_Usuarios_UserId",
                table: "ChatHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatHistory",
                table: "ChatHistory");

            migrationBuilder.RenameTable(
                name: "ChatHistory",
                newName: "ChatHistories");

            migrationBuilder.RenameIndex(
                name: "IX_ChatHistory_UserId",
                table: "ChatHistories",
                newName: "IX_ChatHistories_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatHistories",
                table: "ChatHistories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatHistories_Usuarios_UserId",
                table: "ChatHistories",
                column: "UserId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatHistories_Usuarios_UserId",
                table: "ChatHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatHistories",
                table: "ChatHistories");

            migrationBuilder.RenameTable(
                name: "ChatHistories",
                newName: "ChatHistory");

            migrationBuilder.RenameIndex(
                name: "IX_ChatHistories_UserId",
                table: "ChatHistory",
                newName: "IX_ChatHistory_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatHistory",
                table: "ChatHistory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatHistory_Usuarios_UserId",
                table: "ChatHistory",
                column: "UserId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
