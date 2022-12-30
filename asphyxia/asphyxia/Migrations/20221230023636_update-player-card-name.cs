using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace asphyxia.Migrations
{
    /// <inheritdoc />
    public partial class updateplayercardname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Cards");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Players",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Players");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Cards",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
