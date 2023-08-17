using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recipe.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFavouriteModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApiRecipeId",
                table: "Favourite",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApiRecipeId",
                table: "Favourite");
        }
    }
}
