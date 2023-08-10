using Microsoft.EntityFrameworkCore;
using Recipe.Library.Models;

namespace Recipe.Library.Data
{
    public class RecipeDbContext : DbContext
    {
        public RecipeDbContext(DbContextOptions<RecipeDbContext> options) : base(options)
        {
        }

        public DbSet<RecipeItem> Recipes { get; set; }
    }
}
