using System;
using EncounterForgeSSR.Data;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Infrastructure;

#nullable disable

namespace EncounterForgeSSR.Data.Migrations;

[DbContext(typeof(AppDbContext))]
[Migration("20260602000000_InitialCreate")]
public partial class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Encounters",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Name = table.Column<string>(type: "TEXT", maxLength: 120, nullable: false),
                PartyLevel = table.Column<int>(nullable: false),
                PartySize = table.Column<int>(nullable: false),
                Environment = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                Difficulty = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                Notes = table.Column<string>(type: "TEXT", maxLength: 4000, nullable: true),
                CreatedAtUtc = table.Column<DateTime>(nullable: false),
                UpdatedAtUtc = table.Column<DateTime>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Encounters", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "MonsterEntries",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                EncounterId = table.Column<int>(nullable: false),
                Name = table.Column<string>(type: "TEXT", maxLength: 120, nullable: false),
                Quantity = table.Column<int>(nullable: false),
                ChallengeRating = table.Column<string>(type: "TEXT", nullable: false),
                Notes = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true),
                SortOrder = table.Column<int>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_MonsterEntries", x => x.Id);
                table.ForeignKey(
                    name: "FK_MonsterEntries_Encounters_EncounterId",
                    column: x => x.EncounterId,
                    principalTable: "Encounters",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_MonsterEntries_EncounterId",
            table: "MonsterEntries",
            column: "EncounterId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "MonsterEntries");
        migrationBuilder.DropTable(name: "Encounters");
    }
}