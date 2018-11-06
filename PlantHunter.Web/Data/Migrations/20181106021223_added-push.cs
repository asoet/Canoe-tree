using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace web.Data.Migrations
{
    public partial class addedpush : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeviceUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DeviceId = table.Column<string>(nullable: true),
                    PushNotificationId = table.Column<string>(nullable: true),
                    Pns = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceUsers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceUsers");
        }
    }
}
