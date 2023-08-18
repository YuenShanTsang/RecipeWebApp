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
        public IActionResult Favourite(string sortBy)
        {
            // Retrieve list of favorite API recipes from the database
            List<Favourite> favoriteApiRecipes = _dbContext.Favourites.ToList();

            // Sort recipes based on user selection (timestamp or rating)
            if (sortBy == "timestamp")
            {
                favoriteApiRecipes = favoriteApiRecipes.OrderByDescending(r => r.DateFavourited).ToList();
            }
            else if (sortBy == "rating")
            {
                favoriteApiRecipes = favoriteApiRecipes.OrderByDescending(r => r.Rating).ToList();
            }

            // Display the "Favourite" view with the sorted recipes
            return View(favoriteApiRecipes);
        }

        // GET: /Favourite/Details/5
        public IActionResult Details(string id)
        {
            // Retrieve the details of a favorite API recipe based on the provided API ID
            var favoriteApiRecipe = _dbContext.Favourites.FirstOrDefault(r => r.ApiRecipeId == id);

            // Check if the recipe exists
            if (favoriteApiRecipe == null)
            {
                return NotFound();
            }

            // Display the "Details" view with the recipe details
            return View(favoriteApiRecipe);
        }


        // POST: /Favourite/RemoveFromFavorites
        [HttpPost]
        public IActionResult RemoveFromFavorites(int recipeId)
        {
            // Retrieve the favorite recipe to be removed
            var favoriteRecipe = _dbContext.Favourites.FirstOrDefault(r => r.FavouriteId == recipeId);

            // Check if the recipe exists
            if (favoriteRecipe == null)
            {
                return NotFound();
            }

            // Remove the recipe from favorites and save changes to the database
            _dbContext.Favourites.Remove(favoriteRecipe);
            _dbContext.SaveChanges();

            // Redirect to the "Favourite" action
            return RedirectToAction("Favourite");
        }


        // POST: /Favourite/RateFavoriteRecipe
        [HttpPost]
        public IActionResult RateFavoriteRecipe(int recipeId, int rating)
        {
            // Retrieve the favorite recipe to be rated
            var favoriteRecipe = _dbContext.Favourites.FirstOrDefault(r => r.FavouriteId == recipeId);

            // Check if the recipe exists
            if (favoriteRecipe == null)
            {
                return NotFound();
            }

            // Update rating and number of ratings
            favoriteRecipe.Rating = (favoriteRecipe.Rating * favoriteRecipe.NumberOfRatings + rating) / (favoriteRecipe.NumberOfRatings + 1);
            favoriteRecipe.NumberOfRatings++;

            // Save changes to the database
            _dbContext.SaveChanges();

            // Redirect to the "Index" action
            return RedirectToAction("Index");
        }

    }

}
