using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Report.API.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SensorDeviceId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    DataType = table.Column<string>(maxLength: 20, nullable: false),
                    HealthStatus = table.Column<string>(maxLength: 20, nullable: false),
                    HealthDescription = table.Column<string>(nullable: false),
                    Diseases = table.Column<string>(nullable: true),
                    Accuracy = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reports");
        }
    }
}
