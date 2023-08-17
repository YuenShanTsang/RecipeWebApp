using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recipe.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddTimeFavourite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateFavourited",
                table: "Recipes",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateFavourited",
                table: "Recipes");
        }
    }
}
