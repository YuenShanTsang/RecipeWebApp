using Microsoft.AspNetCore.Mvc;
using Recipe.Library.Models;
using Recipe.WebApp.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Recipe.WebApp.Controllers
{
    public class FavouriteController : Controller
    {
        private readonly RecipeDbContext _dbContext;

        public FavouriteController(RecipeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Favourite()
        {
            // Retrieve list of favorite recipes from the database
            List<RecipeItem> favoriteRecipes = _dbContext.Recipes.Where(r => r.IsFavorite).ToList();

            return View(favoriteRecipes);
        }

    }
}

