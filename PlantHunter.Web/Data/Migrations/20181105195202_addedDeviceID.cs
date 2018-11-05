using Microsoft.EntityFrameworkCore.Migrations;

namespace web.Data.Migrations
{
    public partial class addedDeviceID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeviceId",
                table: "Plants",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "Plants");
        }
    }
}
