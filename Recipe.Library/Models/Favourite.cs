using System.ComponentModel.DataAnnotations;

namespace Recipe.Library.Models
{
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

        public DateTime DateFavourited { get; set; }
    }
}
