using Microsoft.EntityFrameworkCore.Migrations;

namespace Report.API.Infrastructure.Migrations
{
    public partial class ChangeSensorIdToRecordId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SensorDeviceId",
                table: "Reports");

            migrationBuilder.AddColumn<int>(
                name: "RecordId",
                table: "Reports",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecordId",
                table: "Reports");

            migrationBuilder.AddColumn<int>(
                name: "SensorDeviceId",
                table: "Reports",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
