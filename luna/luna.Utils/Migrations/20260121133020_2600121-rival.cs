using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace luna.Migrations
{
    /// <inheritdoc />
    public partial class _2600121rival : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "sv_rivals",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ref_id = table.Column<string>(type: "char(16)", fixedLength: true, maxLength: 16, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    rival_ref_id = table.Column<string>(type: "char(16)", fixedLength: true, maxLength: 16, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sdvx_id = table.Column<int>(type: "int(11)", nullable: false),
                    name = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    mutual = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    version = table.Column<int>(type: "int(11)", nullable: false),
                    ProfileNavigationId = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "FK_sv_rivals_sv_profile_ProfileNavigationId",
                        column: x => x.ProfileNavigationId,
                        principalTable: "sv_profile",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Data store(Rivals) for Sound Voltex")
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateIndex(
                name: "idx_refid_version",
                table: "sv_rivals",
                columns: new[] { "ref_id", "version" });

            migrationBuilder.CreateIndex(
                name: "IX_sv_rivals_ProfileNavigationId",
                table: "sv_rivals",
                column: "ProfileNavigationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sv_rivals");
        }
    }
}
