using System.ComponentModel.DataAnnotations;

namespace Recipe.Library.Models
{
    public class Favourite
    {
        [Key]
        public int FavouriteId { get; set; }

        [Required]
        public int RecipeId { get; set; }

        [Required]
        public RecipeItem Recipe { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        public DateTime DateFavourited { get; set; }
    }
}
