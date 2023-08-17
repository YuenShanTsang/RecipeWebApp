using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recipe.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFavourite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favourites_Recipes_RecipeId",
                table: "Favourites");

            migrationBuilder.DropIndex(
                name: "IX_Favourites_RecipeId",
                table: "Favourites");

            migrationBuilder.DropColumn(
                name: "RecipeId",
                table: "Favourites");

            migrationBuilder.AddColumn<int>(
                name: "RecipeItemRecipeId",
                table: "Favourites",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Favourites_RecipeItemRecipeId",
                table: "Favourites",
                column: "RecipeItemRecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Favourites_Recipes_RecipeItemRecipeId",
                table: "Favourites",
                column: "RecipeItemRecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favourites_Recipes_RecipeItemRecipeId",
                table: "Favourites");

            migrationBuilder.DropIndex(
                name: "IX_Favourites_RecipeItemRecipeId",
                table: "Favourites");

            migrationBuilder.DropColumn(
                name: "RecipeItemRecipeId",
                table: "Favourites");

            migrationBuilder.AddColumn<int>(
                name: "RecipeId",
                table: "Favourites",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Favourites_RecipeId",
                table: "Favourites",
                column: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Favourites_Recipes_RecipeId",
                table: "Favourites",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
