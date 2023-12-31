﻿using System.ComponentModel.DataAnnotations;

namespace Recipe.Library.Models
{
    // Model of a recipe item.
    public class RecipeItem
    {
        [Key]
        public int RecipeId { get; set; }

        [Required]
        [StringLength(100)]
        public string RecipeName { get; set; } = null!;

        [Required]
        [StringLength(500)]
        public string RecipeCategory { get; set; } = null!;

        [Required]
        [StringLength(500)]
        public string RecipeIngredient { get; set; } = null!;

        [Required]
        [StringLength(1000)]
        public string RecipeInstruction { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string RecipeCookingTime { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string RecipeDifficulty { get; set; } = null!;

        [StringLength(200)]
        public string RecipeImage { get; set; } = "";

        [StringLength(50)]
        public string ApiRecipeId { get; set; } = "";

        public bool IsFavorite { get; set; }

        public DateTime? DateFavourited { get; set; }

        public double Rating { get; set; }

        public int NumberOfRatings { get; set; }

        [EnumDataType(typeof(RecipeOperation))]
        public RecipeOperation Operation;

        public virtual ICollection<Favourite> Favourites { get; set; } = new List<Favourite>();
        public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();
    }

    public enum RecipeOperation
    {
        Create,
        Update,
        Delete
    }
}
