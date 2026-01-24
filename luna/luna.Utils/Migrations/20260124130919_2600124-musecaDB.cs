using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace luna.Migrations
{
    /// <inheritdoc />
    public partial class _2600124musecaDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "museca_profile",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hidden_param = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    play_count = table.Column<int>(type: "int(11)", nullable: false),
                    daily_count = table.Column<int>(type: "int(11)", nullable: false),
                    play_chain = table.Column<int>(type: "int(11)", nullable: false),
                    headphone = table.Column<byte>(type: "tinyint(3) unsigned", nullable: false),
                    appeal_id = table.Column<int>(type: "int(11)", nullable: false),
                    comment_id = table.Column<int>(type: "int(11)", nullable: false),
                    music_id = table.Column<int>(type: "int(11)", nullable: false),
                    music_type = table.Column<byte>(type: "tinyint(3) unsigned", nullable: false),
                    sort_type = table.Column<byte>(type: "tinyint(3) unsigned", nullable: false),
                    narrow_down = table.Column<byte>(type: "tinyint(3) unsigned", nullable: false),
                    gauge_option = table.Column<byte>(type: "tinyint(3) unsigned", nullable: false),
                    blaster_energy = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    blaster_count = table.Column<uint>(type: "int(10) unsigned", nullable: false),
                    creator_id = table.Column<int>(type: "int(11)", nullable: false),
                    skill_level = table.Column<short>(type: "smallint(6)", nullable: false),
                    skill_name_id = table.Column<short>(type: "smallint(6)", nullable: false),
                    gamecoin_packet = table.Column<int>(type: "int(11)", nullable: false),
                    gamecoin_block = table.Column<int>(type: "int(11)", nullable: false),
                    packet_booster = table.Column<int>(type: "int(11)", nullable: false),
                    block_booster = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                },
                comment: "Data store(Profile) for Museca")
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "museca_items",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    profile = table.Column<int>(type: "int(11)", nullable: false),
                    item_id = table.Column<int>(type: "int(11)", nullable: false),
                    type = table.Column<int>(type: "int(11)", nullable: false),
                    param = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "FK_museca_profile_to_profile(id)",
                        column: x => x.profile,
                        principalTable: "museca_profile",
                        principalColumn: "id");
                },
                comment: "Data store(Items) for Museca")
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateTable(
                name: "museca_scores",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    profile = table.Column<int>(type: "int(11)", nullable: false),
                    music_id = table.Column<int>(type: "int(11)", nullable: false),
                    type = table.Column<int>(type: "int(11)", nullable: false),
                    score = table.Column<int>(type: "int(11)", nullable: false),
                    count = table.Column<int>(type: "int(11)", nullable: false),
                    clear_type = table.Column<int>(type: "int(11)", nullable: false),
                    score_grade = table.Column<int>(type: "int(11)", nullable: false),
                    btn_rate = table.Column<int>(type: "int(11)", nullable: false),
                    long_rate = table.Column<int>(type: "int(11)", nullable: false),
                    vol_rate = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "FK_museca_score_profile_to_profile(id)",
                        column: x => x.profile,
                        principalTable: "museca_profile",
                        principalColumn: "id");
                },
                comment: "Data store(Scores) for Museca")
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_general_ci");

            migrationBuilder.CreateIndex(
                name: "FK_museca_profile_to_profile(id)",
                table: "museca_items",
                column: "profile");

            migrationBuilder.CreateIndex(
                name: "FK_museca_score_profile_to_profile(id)",
                table: "museca_scores",
                column: "profile");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "museca_items");

            migrationBuilder.DropTable(
                name: "museca_scores");

            migrationBuilder.DropTable(
                name: "museca_profile");
        }
    }
}
