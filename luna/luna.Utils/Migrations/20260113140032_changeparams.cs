using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace asphyxia.Migrations
{
    /// <inheritdoc />
    public partial class changeparams : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "param",
                table: "sv_params",
                type: "longtext",
                maxLength: -1,
                nullable: false,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(int),
                oldType: "int(11)")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "param",
                table: "sv_params",
                type: "int(11)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldMaxLength: -1)
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_general_ci");
        }
    }
}
