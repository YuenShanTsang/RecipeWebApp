using System;
using System.ComponentModel.DataAnnotations;

namespace Recipe.WebApp.Models
{
	public class Rating
	{
        [Key]
        public int RatingId { get; set; }

        public int RecipeId { get; set; }
    }
}

