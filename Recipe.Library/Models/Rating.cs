using System.ComponentModel.DataAnnotations;

namespace Recipe.Library.Models
{
    // Model of a user rating for a recipe
    public class Rating
    {
        [Key]
        public int RatingId { get; set; }

        [Required]
        public int RecipeId { get; set; }

        [Required]
        public RecipeItem Recipe { get; set; } = null!;

        [Range(1, 5, ErrorMessage = "Stars must be between 1 and 5.")]
        public int Stars { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateRated { get; set; }
    }
}
