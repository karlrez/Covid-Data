using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace assignment1.Data.Migrations
{
    public partial class secondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.CreateTable(
                name: "DailyCovidData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    pruid = table.Column<int>(type: "INTEGER", nullable: false),
                    prname = table.Column<string>(type: "TEXT", nullable: true),
                    prnameFR = table.Column<string>(type: "TEXT", nullable: true),
                    date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    numconf = table.Column<int>(type: "INTEGER", nullable: false),
                    numprob = table.Column<int>(type: "INTEGER", nullable: false),
                    numdeaths = table.Column<int>(type: "INTEGER", nullable: false),
                    numtotal = table.Column<int>(type: "INTEGER", nullable: false),
                    numtoday = table.Column<int>(type: "INTEGER", nullable: false),
                    ratetotal = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyCovidData", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyCovidData");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }
    }
}
