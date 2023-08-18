using Microsoft.EntityFrameworkCore;
using Recipe.Library.Models;

namespace Recipe.WebApp.Models
{
    public class RecipeDbContext : DbContext
    {
        public RecipeDbContext(DbContextOptions<RecipeDbContext> options) : base(options)
        {
        }

        // Gets or sets the DbSet for managing user-created recipes.
        public DbSet<RecipeItem> Recipes { get; set; }

        // Gets or sets the DbSet for managing favorite recipes.
        public DbSet<Favourite> Favourites { get; set; }
    }
}
