using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dotel2.Migrations
{
    public partial class AddChatModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "UserMemberships");

            migrationBuilder.CreateTable(
                name: "UserConversationReads",
                columns: table => new
                {
                    ConversationId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LastReadAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserConversationReads", x => new { x.ConversationId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserConversationReads_Conversations_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "Conversations",
                        principalColumn: "ConversationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserConversationReads_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserConversationReads_UserId",
                table: "UserConversationReads",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserConversationReads");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "UserMemberships",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
