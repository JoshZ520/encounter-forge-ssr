using System;
using EncounterForgeSSR.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EncounterForgeSSR.Data.Migrations;

[DbContext(typeof(AppDbContext))]
[Migration("20260603170000_AddAuthAndMonsterCatalog")]
public partial class AddAuthAndMonsterCatalog : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "AppUsers",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                UserName = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                PasswordHash = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                CreatedAtUtc = table.Column<DateTime>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AppUsers", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "MonsterCatalogs",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Name = table.Column<string>(type: "TEXT", maxLength: 120, nullable: false),
                ChallengeRating = table.Column<string>(type: "TEXT", nullable: false),
                MonsterType = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                SuggestedNotes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_MonsterCatalogs", x => x.Id);
            });

        migrationBuilder.AddColumn<int>(
            name: "MonsterCatalogId",
            table: "MonsterEntries",
            nullable: true);

        migrationBuilder.CreateIndex(
            name: "IX_AppUsers_UserName",
            table: "AppUsers",
            column: "UserName",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_MonsterEntries_MonsterCatalogId",
            table: "MonsterEntries",
            column: "MonsterCatalogId");

    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "AppUsers");
        migrationBuilder.DropTable(name: "MonsterCatalogs");

        migrationBuilder.DropIndex(
            name: "IX_MonsterEntries_MonsterCatalogId",
            table: "MonsterEntries");

        migrationBuilder.DropColumn(
            name: "MonsterCatalogId",
            table: "MonsterEntries");
    }
}
