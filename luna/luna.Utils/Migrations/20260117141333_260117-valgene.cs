using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace luna.Migrations
{
    /// <inheritdoc />
    public partial class _260117valgene : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "sv_valgene_tickets",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    profile = table.Column<int>(type: "int(11)", nullable: false),
                    ticket_num = table.Column<int>(type: "int(11)", nullable: false),
                    limit_date = table.Column<ulong>(type: "bigint(20) unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "FK_valgene_profile_to_profile(id)",
                        column: x => x.profile,
                        principalTable: "sv_profile",
                        principalColumn: "id");
                },
                comment: "Data store(Valgene Tickets) for Sound Voltex")
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateIndex(
                name: "FK_valgene_profile_to_profile(id)",
                table: "sv_valgene_tickets",
                column: "profile");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sv_valgene_tickets");
        }
    }
}
