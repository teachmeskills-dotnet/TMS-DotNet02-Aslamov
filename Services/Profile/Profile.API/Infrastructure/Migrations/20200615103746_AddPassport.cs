using Microsoft.EntityFrameworkCore.Migrations;

namespace Profile.API.Infrastructure.Migrations
{
    public partial class AddPassport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Passport",
                table: "Profiles",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Passport",
                table: "Profiles");
        }
    }
}
