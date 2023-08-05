using System;
using System.ComponentModel.DataAnnotations;

namespace Recipe.WebApp.Models
{
	public class Favourite
	{
        [Key]
        public int FavouriteId { get; set; }

        public int RecipeId { get; set; }
    }
}

