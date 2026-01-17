using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace luna.Migrations
{
    /// <inheritdoc />
    public partial class _260116 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "sv_profile",
                type: "TEXT",
                maxLength: 8,
                nullable: false,
                defaultValueSql: "'VOLTEX'",
                oldClrType: typeof(string),
                oldType: "varchar(8)",
                oldMaxLength: 8,
                oldDefaultValueSql: "'VOLTEX'");

            migrationBuilder.AlterColumn<string>(
                name: "kac_id",
                table: "sv_profile",
                type: "TEXT",
                maxLength: 8,
                nullable: false,
                defaultValueSql: "'VOLTEX'",
                oldClrType: typeof(string),
                oldType: "varchar(8)",
                oldMaxLength: 8,
                oldDefaultValueSql: "'VOLTEX'");

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "sv_profile",
                type: "TEXT",
                fixedLength: true,
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(10)",
                oldFixedLength: true,
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "param",
                table: "sv_params",
                type: "TEXT",
                maxLength: -1,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldMaxLength: -1);

            migrationBuilder.AlterColumn<string>(
                name: "title_yomigana",
                table: "sv_music",
                type: "TEXT",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "sv_music",
                type: "TEXT",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "date",
                table: "sv_music",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<string>(
                name: "artist_yomigana",
                table: "sv_music",
                type: "TEXT",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "artist",
                table: "sv_music",
                type: "TEXT",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "region_name",
                table: "facility",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "region_jname",
                table: "facility",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "region",
                table: "facility",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "pcb_id",
                table: "facility",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "facility",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "facility_id",
                table: "facility",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "customer_code",
                table: "facility",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "country_name",
                table: "facility",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "country_jname",
                table: "facility",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "country",
                table: "facility",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "company_code",
                table: "facility",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "ref_id",
                table: "card",
                type: "TEXT",
                fixedLength: true,
                maxLength: 16,
                nullable: false,
                comment: "same with dataid",
                oldClrType: typeof(string),
                oldType: "char(16)",
                oldFixedLength: true,
                oldMaxLength: 16,
                oldComment: "same with dataid");

            migrationBuilder.AlterColumn<string>(
                name: "pass",
                table: "card",
                type: "TEXT",
                fixedLength: true,
                maxLength: 4,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(4)",
                oldFixedLength: true,
                oldMaxLength: 4);

            migrationBuilder.AlterColumn<string>(
                name: "paseli_session",
                table: "card",
                type: "TEXT",
                fixedLength: true,
                maxLength: 16,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(16)",
                oldFixedLength: true,
                oldMaxLength: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "card_no",
                table: "card",
                type: "TEXT",
                fixedLength: true,
                maxLength: 16,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(16)",
                oldFixedLength: true,
                oldMaxLength: 16);

            migrationBuilder.AlterColumn<string>(
                name: "card_id",
                table: "card",
                type: "TEXT",
                fixedLength: true,
                maxLength: 16,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(16)",
                oldFixedLength: true,
                oldMaxLength: 16);

            migrationBuilder.AlterColumn<string>(
                name: "ProductVersion",
                table: "__efmigrationshistory",
                type: "TEXT",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(32)",
                oldMaxLength: 32);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "sv_profile",
                type: "varchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValueSql: "'VOLTEX'",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 8,
                oldDefaultValueSql: "'VOLTEX'");

            migrationBuilder.AlterColumn<string>(
                name: "kac_id",
                table: "sv_profile",
                type: "varchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValueSql: "'VOLTEX'",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 8,
                oldDefaultValueSql: "'VOLTEX'");

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "sv_profile",
                type: "char(10)",
                fixedLength: true,
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldFixedLength: true,
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "param",
                table: "sv_params",
                type: "longtext",
                maxLength: -1,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: -1);

            migrationBuilder.AlterColumn<string>(
                name: "title_yomigana",
                table: "sv_music",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "sv_music",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "date",
                table: "sv_music",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "artist_yomigana",
                table: "sv_music",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "artist",
                table: "sv_music",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "region_name",
                table: "facility",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "region_jname",
                table: "facility",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "region",
                table: "facility",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "pcb_id",
                table: "facility",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "facility",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "facility_id",
                table: "facility",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "customer_code",
                table: "facility",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "country_name",
                table: "facility",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "country_jname",
                table: "facility",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "country",
                table: "facility",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "company_code",
                table: "facility",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "ref_id",
                table: "card",
                type: "char(16)",
                fixedLength: true,
                maxLength: 16,
                nullable: false,
                comment: "same with dataid",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldFixedLength: true,
                oldMaxLength: 16,
                oldComment: "same with dataid");

            migrationBuilder.AlterColumn<string>(
                name: "pass",
                table: "card",
                type: "char(4)",
                fixedLength: true,
                maxLength: 4,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldFixedLength: true,
                oldMaxLength: 4);

            migrationBuilder.AlterColumn<string>(
                name: "paseli_session",
                table: "card",
                type: "char(16)",
                fixedLength: true,
                maxLength: 16,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldFixedLength: true,
                oldMaxLength: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "card_no",
                table: "card",
                type: "char(16)",
                fixedLength: true,
                maxLength: 16,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldFixedLength: true,
                oldMaxLength: 16);

            migrationBuilder.AlterColumn<string>(
                name: "card_id",
                table: "card",
                type: "char(16)",
                fixedLength: true,
                maxLength: 16,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldFixedLength: true,
                oldMaxLength: 16);

            migrationBuilder.AlterColumn<string>(
                name: "ProductVersion",
                table: "__efmigrationshistory",
                type: "varchar(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<string>(
                name: "MigrationId",
                table: "__efmigrationshistory",
                type: "varchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 150);
        }
    }
}
