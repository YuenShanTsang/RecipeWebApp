using System.ComponentModel.DataAnnotations;

namespace Recipe.WebApp.Models
{
	public class Recipe
	{
        [Key]
        public int RecipeId { get; set; }

        //public string RecipeName { get; set; }

        //public string RecipeIngredient { get; set; }

        //public string RecipeInstruction { get; set; }

        //public string RecipeCookingTime { get; set; }

        //public string RecipeDifficulty { get; set; }

        //public string RecipeImage { get; set; }

        public RecipeOperation Operation;

    }

    public enum RecipeOperation
    {
        Create,
        Update,
        Delete
    }
}

