using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace luna.Migrations
{
    /// <inheritdoc />
    public partial class _260117courserecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "sv_course_records",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    profile = table.Column<int>(type: "int(11)", nullable: false),
                    series_id = table.Column<int>(type: "int(11)", nullable: false),
                    course_id = table.Column<int>(type: "int(11)", nullable: false),
                    version = table.Column<int>(type: "int(11)", nullable: false),
                    score = table.Column<int>(type: "int(11)", nullable: false),
                    clear = table.Column<int>(type: "int(11)", nullable: false),
                    grade = table.Column<int>(type: "int(11)", nullable: false),
                    rate = table.Column<int>(type: "int(11)", nullable: false),
                    count = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "FK_course_profile_to_profile(id)",
                        column: x => x.profile,
                        principalTable: "sv_profile",
                        principalColumn: "id");
                },
                comment: "Data store(Course Records) for Sound Voltex")
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateIndex(
                name: "FK_course_profile_to_profile(id)",
                table: "sv_course_records",
                column: "profile");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sv_course_records");
        }
    }
}
