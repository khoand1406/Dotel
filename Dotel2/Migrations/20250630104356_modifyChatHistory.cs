using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dotel2.Migrations
{
    public partial class modifyChatHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserMessage",
                table: "ChatHistories");

            migrationBuilder.RenameColumn(
                name: "BotReply",
                table: "ChatHistories",
                newName: "Message");

            migrationBuilder.AddColumn<string>(
                name: "Sender",
                table: "ChatHistories",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sender",
                table: "ChatHistories");

            migrationBuilder.RenameColumn(
                name: "Message",
                table: "ChatHistories",
                newName: "BotReply");

            migrationBuilder.AddColumn<string>(
                name: "UserMessage",
                table: "ChatHistories",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");
        }
    }
}
