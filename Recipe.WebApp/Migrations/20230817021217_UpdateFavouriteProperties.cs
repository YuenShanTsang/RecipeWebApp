using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recipe.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFavouriteProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favourite_Recipes_RecipeId",
                table: "Favourite");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Favourite",
                table: "Favourite");

            migrationBuilder.RenameTable(
                name: "Favourite",
                newName: "Favourites");

            migrationBuilder.RenameIndex(
                name: "IX_Favourite_RecipeId",
                table: "Favourites",
                newName: "IX_Favourites_RecipeId");

            migrationBuilder.AddColumn<string>(
                name: "RecipeArea",
                table: "Favourites",
                type: "TEXT",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RecipeCategory",
                table: "Favourites",
                type: "TEXT",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RecipeImage",
                table: "Favourites",
                type: "TEXT",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RecipeInstruction",
                table: "Favourites",
                type: "TEXT",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RecipeName",
                table: "Favourites",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Favourites",
                table: "Favourites",
                column: "FavouriteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Favourites_Recipes_RecipeId",
                table: "Favourites",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favourites_Recipes_RecipeId",
                table: "Favourites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Favourites",
                table: "Favourites");

            migrationBuilder.DropColumn(
                name: "RecipeArea",
                table: "Favourites");

            migrationBuilder.DropColumn(
                name: "RecipeCategory",
                table: "Favourites");

            migrationBuilder.DropColumn(
                name: "RecipeImage",
                table: "Favourites");

            migrationBuilder.DropColumn(
                name: "RecipeInstruction",
                table: "Favourites");

            migrationBuilder.DropColumn(
                name: "RecipeName",
                table: "Favourites");

            migrationBuilder.RenameTable(
                name: "Favourites",
                newName: "Favourite");

            migrationBuilder.RenameIndex(
                name: "IX_Favourites_RecipeId",
                table: "Favourite",
                newName: "IX_Favourite_RecipeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Favourite",
                table: "Favourite",
                column: "FavouriteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Favourite_Recipes_RecipeId",
                table: "Favourite",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
