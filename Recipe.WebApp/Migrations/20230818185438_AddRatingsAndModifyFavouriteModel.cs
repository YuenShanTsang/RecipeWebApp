using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recipe.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddRatingsAndModifyFavouriteModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfRatings",
                table: "Favourites",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Favourites",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfRatings",
                table: "Favourites");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Favourites");
        }
    }
}
