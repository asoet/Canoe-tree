using Microsoft.EntityFrameworkCore.Migrations;

namespace web.Data.Migrations
{
    public partial class addedExtraInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Plants",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EndangeredLevel",
                table: "Plants",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Family",
                table: "Plants",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ScientificName",
                table: "Plants",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surrounding",
                table: "Plants",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "EndangeredLevel",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "Family",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "ScientificName",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "Surrounding",
                table: "Plants");
        }
    }
}
