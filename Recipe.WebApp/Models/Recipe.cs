using System.ComponentModel.DataAnnotations;

namespace Recipe.WebApp.Models
{
	public class Recipe
	{
        [Key]
        public int RecipeId { get; set; }

    }
}

