using System.ComponentModel.DataAnnotations;

namespace Recipe.Library.Models
{
    // Model of a favorite recipe
    public class Favourite
    {
        [Key]
        public int FavouriteId { get; set; }

        [Required]
        public string ApiRecipeId { get; set; } = "";

        [StringLength(100)]
        public string RecipeName { get; set; } = null!;


        [StringLength(500)]
        public string RecipeCategory { get; set; } = null!;


        [StringLength(500)]
        public string RecipeArea { get; set; } = null!;

  
        [StringLength(1000)]
        public string RecipeInstruction { get; set; } = null!;

        [StringLength(200)]
        public string RecipeImage { get; set; } = "";

        [DataType(DataType.DateTime)]
        public DateTime DateFavourited { get; set; }

        public double Rating { get; set; }

        public int NumberOfRatings { get; set; }
    }
}
