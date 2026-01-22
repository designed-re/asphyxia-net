using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace luna.Migrations
{
    /// <inheritdoc />
    public partial class _2600122versioning3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "version",
                table: "sv_scores",
                type: "int(11)",
                nullable: false,
                defaultValue: 6,
                oldClrType: typeof(int),
                oldType: "int(11)");

            migrationBuilder.AlterColumn<int>(
                name: "version",
                table: "sv_rivals",
                type: "int(11)",
                nullable: false,
                defaultValue: 6,
                oldClrType: typeof(int),
                oldType: "int(11)");

            migrationBuilder.AlterColumn<int>(
                name: "version",
                table: "sv_profile",
                type: "int(11)",
                nullable: false,
                defaultValue: 6,
                oldClrType: typeof(int),
                oldType: "int(11)");

            migrationBuilder.AlterColumn<int>(
                name: "version",
                table: "sv_params",
                type: "int(11)",
                nullable: false,
                defaultValue: 6,
                oldClrType: typeof(int),
                oldType: "int(11)");

            migrationBuilder.AlterColumn<int>(
                name: "version",
                table: "sv_music",
                type: "int(11)",
                nullable: false,
                defaultValue: 6,
                oldClrType: typeof(int),
                oldType: "int(11)");

            migrationBuilder.AlterColumn<int>(
                name: "version",
                table: "sv_items",
                type: "int(11)",
                nullable: false,
                defaultValue: 6,
                oldClrType: typeof(int),
                oldType: "int(11)");

            migrationBuilder.AlterColumn<int>(
                name: "version",
                table: "sv_events",
                type: "int(11)",
                nullable: false,
                defaultValue: 6,
                oldClrType: typeof(int),
                oldType: "int(11)");

            migrationBuilder.AlterColumn<int>(
                name: "version",
                table: "sv_course_records",
                type: "int(11)",
                nullable: false,
                defaultValue: 6,
                oldClrType: typeof(int),
                oldType: "int(11)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "version",
                table: "sv_scores",
                type: "int(11)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int(11)",
                oldDefaultValue: 6);

            migrationBuilder.AlterColumn<int>(
                name: "version",
                table: "sv_rivals",
                type: "int(11)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int(11)",
                oldDefaultValue: 6);

            migrationBuilder.AlterColumn<int>(
                name: "version",
                table: "sv_profile",
                type: "int(11)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int(11)",
                oldDefaultValue: 6);

            migrationBuilder.AlterColumn<int>(
                name: "version",
                table: "sv_params",
                type: "int(11)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int(11)",
                oldDefaultValue: 6);

            migrationBuilder.AlterColumn<int>(
                name: "version",
                table: "sv_music",
                type: "int(11)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int(11)",
                oldDefaultValue: 6);

            migrationBuilder.AlterColumn<int>(
                name: "version",
                table: "sv_items",
                type: "int(11)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int(11)",
                oldDefaultValue: 6);

            migrationBuilder.AlterColumn<int>(
                name: "version",
                table: "sv_events",
                type: "int(11)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int(11)",
                oldDefaultValue: 6);

            migrationBuilder.AlterColumn<int>(
                name: "version",
                table: "sv_course_records",
                type: "int(11)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int(11)",
                oldDefaultValue: 6);
        }
    }
}
