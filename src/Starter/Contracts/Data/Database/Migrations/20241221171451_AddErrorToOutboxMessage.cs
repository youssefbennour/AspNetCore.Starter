using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Starter.Contracts.Data.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddErrorToOutboxMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Error",
                schema: "Contracts",
                table: "OutboxMessage",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Error",
                schema: "Contracts",
                table: "OutboxMessage");
        }
    }
}
