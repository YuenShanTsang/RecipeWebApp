using System.ComponentModel.DataAnnotations;

namespace Recipe.Library.Models
{
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
        public DateTime DateRated { get; set; }
    }
}
