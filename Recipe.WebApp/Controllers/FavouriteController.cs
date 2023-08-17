using Microsoft.AspNetCore.Mvc;
using Recipe.Library.Models;
using Recipe.WebApp.Models;

namespace Recipe.WebApp.Controllers
{
    public class FavouriteController : Controller
    {
        private readonly RecipeDbContext _dbContext;

        public FavouriteController(RecipeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: /Favourite/
        public IActionResult Favourite()
        {
            // Retrieve list of favorite API recipes from the database
            List<Favourite> favoriteApiRecipes = _dbContext.Favourites.ToList();

            return View(favoriteApiRecipes);
        }

        // GET: /Favourite/Details/5
        public IActionResult Details(string id)
        {
            var favoriteApiRecipe = _dbContext.Favourites.FirstOrDefault(r => r.ApiRecipeId == id);

            if (favoriteApiRecipe == null)
            {
                return NotFound();
            }

            return View(favoriteApiRecipe);
        }


        // POST: /Favourite/RemoveFromFavorites
        [HttpPost]
        public IActionResult RemoveFromFavorites(int recipeId)
        {
            var favoriteRecipe = _dbContext.Favourites.FirstOrDefault(r => r.FavouriteId == recipeId);

            if (favoriteRecipe == null)
            {
                return NotFound();
            }

            _dbContext.Favourites.Remove(favoriteRecipe);
            _dbContext.SaveChanges();

            return RedirectToAction("Favourite");
        }
    }
}
