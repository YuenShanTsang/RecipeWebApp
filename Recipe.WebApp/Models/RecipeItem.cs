using System.ComponentModel.DataAnnotations;

namespace Recipe.WebApp.Models
{
    public class RecipeItem
    {
        [Key]
        public int RecipeId { get; set; }

        public string RecipeName { get; set; } = null!;

        public string RecipeIngredient { get; set; } = null!;

        public string RecipeInstruction { get; set; } = null!;

        public string RecipeCookingTime { get; set; } = null!;

        public string RecipeDifficulty { get; set; } = null!;

        public string RecipeImage { get; set; } = "";

        public RecipeOperation Operation;
    }

    public enum RecipeOperation
    {
        Create,
        Update,
        Delete
    }
}
