using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Starter.Offers.Data.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddErrorToOutboxMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Error",
                schema: "Offers",
                table: "OutboxMessage",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Error",
                schema: "Offers",
                table: "OutboxMessage");
        }
    }
}
