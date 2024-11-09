using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Starter.Offers.Data.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddOutboxTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OutboxMessage",
                schema: "Offers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Message = table.Column<string>(type: "json", nullable: false),
                    SavedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ExecutedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxMessage", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OutboxMessage_ExecutedOn",
                schema: "Offers",
                table: "OutboxMessage",
                column: "ExecutedOn");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OutboxMessage",
                schema: "Offers");
        }
    }
}
