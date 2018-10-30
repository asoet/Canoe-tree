﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace web.Data.Migrations
{
    public partial class geotagging : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Plants",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Plants",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Plants");
        }
    }
}
