using Microsoft.EntityFrameworkCore.Migrations;
using Core.Models;

#nullable disable

namespace Core.Migrations;

public partial class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Players",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Name = table.Column<string>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Players", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Armies",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Name = table.Column<string>(type: "TEXT", nullable: false),
                PlayerId = table.Column<int>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Armies", x => x.Id);
                table.ForeignKey(
                    name: "FK_Armies_Players_PlayerId",
                    column: x => x.PlayerId,
                    principalTable: "Players",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Buildings",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Produces = table.Column<int>(type: "INTEGER", nullable: false),
                ProductionRate = table.Column<int>(type: "INTEGER", nullable: false),
                PlayerId = table.Column<int>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Buildings", x => x.Id);
                table.ForeignKey(
                    name: "FK_Buildings_Players_PlayerId",
                    column: x => x.PlayerId,
                    principalTable: "Players",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Resources",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Type = table.Column<int>(type: "INTEGER", nullable: false),
                Amount = table.Column<int>(type: "INTEGER", nullable: false),
                PlayerId = table.Column<int>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Resources", x => x.Id);
                table.ForeignKey(
                    name: "FK_Resources_Players_PlayerId",
                    column: x => x.PlayerId,
                    principalTable: "Players",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "UnitTypes",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Name = table.Column<string>(type: "TEXT", nullable: false),
                Cost = table.Column<string>(type: "TEXT", nullable: false),
                ManpowerCost = table.Column<int>(type: "INTEGER", nullable: false),
                Attack = table.Column<float>(type: "REAL", nullable: false),
                Defense = table.Column<float>(type: "REAL", nullable: false),
                Speed = table.Column<float>(type: "REAL", nullable: false),
                TrainingTimeSeconds = table.Column<int>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UnitTypes", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "UnitStacks",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                TypeId = table.Column<int>(type: "INTEGER", nullable: false),
                ArmyId = table.Column<int>(type: "INTEGER", nullable: false),
                Quantity = table.Column<int>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UnitStacks", x => x.Id);
                table.ForeignKey(
                    name: "FK_UnitStacks_Armies_ArmyId",
                    column: x => x.ArmyId,
                    principalTable: "Armies",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_UnitStacks_UnitTypes_TypeId",
                    column: x => x.TypeId,
                    principalTable: "UnitTypes",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Armies_PlayerId",
            table: "Armies",
            column: "PlayerId");
        migrationBuilder.CreateIndex(
            name: "IX_Buildings_PlayerId",
            table: "Buildings",
            column: "PlayerId");
        migrationBuilder.CreateIndex(
            name: "IX_Resources_PlayerId",
            table: "Resources",
            column: "PlayerId");
        migrationBuilder.CreateIndex(
            name: "IX_UnitStacks_ArmyId",
            table: "UnitStacks",
            column: "ArmyId");
        migrationBuilder.CreateIndex(
            name: "IX_UnitStacks_TypeId",
            table: "UnitStacks",
            column: "TypeId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "Buildings");
        migrationBuilder.DropTable(name: "Resources");
        migrationBuilder.DropTable(name: "UnitStacks");
        migrationBuilder.DropTable(name: "Armies");
        migrationBuilder.DropTable(name: "UnitTypes");
        migrationBuilder.DropTable(name: "Players");
    }
}
