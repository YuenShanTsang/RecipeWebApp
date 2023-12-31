﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Recipe.Library.Services;
using Recipe.Library.Models;
using Recipe.WebApp.Models;
using Newtonsoft.Json;

namespace Recipe.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly RecipeDbContext _dbContext;
        private readonly IApiService _apiService;

        public HomeController(RecipeDbContext dbContext, IApiService apiService)
        {
            _dbContext = dbContext;
            _apiService = apiService;
        }

        // GET: /Home/Index
        public async Task<IActionResult> Index(string searchQuery)
        {
            // Retrieve user-created recipes from the database
            List<RecipeItem> userRecipes = _dbContext.Recipes.ToList();

            // Retrieve API recipes
            List<RecipeItem> apiRecipes = new List<RecipeItem>();

            for (int i = 0; i < 30; i++)
            {
                string randomMealData = await _apiService.GetRandomMealAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(randomMealData);

                if (apiResponse.Meals != null && apiResponse.Meals.Count > 0)
                {
                    var apiRecipe = apiResponse.Meals[0];
                    apiRecipes.Add(new RecipeItem
                    {
                        RecipeName = apiResponse.Meals[0].StrMeal,
                        RecipeCategory = apiResponse.Meals[0].strCategory,
                        RecipeImage = apiResponse.Meals[0].StrMealThumb,
                        ApiRecipeId = apiResponse.Meals[0].idMeal
                    });
                }
            }

            // Filter out user-created recipes from API recipes
            List<RecipeItem> apiRecipesFiltered = apiRecipes
                .Where(apiRecipe => !userRecipes.Any(userRecipe => userRecipe.ApiRecipeId == apiRecipe.ApiRecipeId))
                .ToList();

            // Combine user-created and filtered API recipes into a single list
            List<RecipeItem> allRecipes = userRecipes.Concat(apiRecipesFiltered).ToList();

            // Apply search filter if a search query is provided
            if (!string.IsNullOrEmpty(searchQuery))
            {
                searchQuery = searchQuery.ToLower();
                allRecipes = allRecipes
                    .Where(recipe =>
                        recipe.RecipeName.ToLower().Contains(searchQuery) ||
                        recipe.RecipeCategory.ToLower().Contains(searchQuery))
                    .ToList();
            }

            // Reorder the list of recipes based on IsFavorite and DateFavourited
            allRecipes = allRecipes
                .OrderByDescending(r => r.IsFavorite)
                .ThenByDescending(r => r.DateFavourited)
                .ToList();

            return View(allRecipes);
        }

        // GET: /Home/Create
        public IActionResult Create()
        {
            // Display the Create view for adding a new recipe
            return View();
        }

        // POST: /Home/CreateRecipe
        [HttpPost]
        [ActionName("CreateRecipe")]
        public IActionResult CreateRecipe(RecipeItem recipe)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(recipe.ApiRecipeId))
                {
                    // The recipe is user-created, just add it to the database
                    _dbContext.Recipes.Add(recipe);
                    _dbContext.SaveChanges();
                }
                else
                {
                    // Check if the API recipe already exists in the database by ApiRecipeId
                    var existingApiRecipe = _dbContext.Recipes.FirstOrDefault(r => r.ApiRecipeId == recipe.ApiRecipeId);

                    if (existingApiRecipe == null)
                    {
                        // The API recipe is not in the database, add it
                        _dbContext.Recipes.Add(recipe);
                        _dbContext.SaveChanges();
                    }
                }

                // Redirect to the Index action to display the updated list of recipes
                return RedirectToAction("Index");
            }
            else
            {
                // Handle validation errors
                return View("~/Views/Create/Create.cshtml", recipe);
            }
        }

        // GET: /Home/Details/5
        public IActionResult Details(int id)
        {
            // Retrieve the recipe details from the database based on the recipe ID
            var recipe = _dbContext.Recipes.FirstOrDefault(r => r.RecipeId == id);

            if (recipe == null)
            {
                return NotFound(); // Handle the case where the recipe is not found
            }

            // Determine the view to render based on the recipe type
            if (string.IsNullOrEmpty(recipe.ApiRecipeId))
            {
                return View("Details", recipe);
            }
            else
            {
                return View("ApiDetails", recipe);
            }
        }

        // GET: /Home/ApiDetails/abc123
        public async Task<IActionResult> ApiDetails(string id)
        {
            // Check if the provided ID corresponds to a user-created recipe or an API recipe
            RecipeItem recipe = _dbContext.Recipes.FirstOrDefault(r => r.ApiRecipeId == id);

            if (recipe == null)
            {
                // Handle the case where the recipe is not found in the user-created recipes
                // Fetch API recipe details and create a temporary RecipeItem object
                string apiRecipeData = await _apiService.GetRecipeByIdAsync(id);

                if (!string.IsNullOrEmpty(apiRecipeData))
                {
                    ApiResponse apiRecipeResponse = JsonConvert.DeserializeObject<ApiResponse>(apiRecipeData);

                    if (apiRecipeResponse.Meals != null && apiRecipeResponse.Meals.Count > 0)
                    {
                        var apiRecipe = apiRecipeResponse.Meals[0];
                        recipe = new RecipeItem
                        {
                            RecipeName = apiRecipe.StrMeal,
                            RecipeCategory = apiRecipe.strCategory,
                            RecipeImage = apiRecipe.StrMealThumb,
                            RecipeInstruction = apiRecipe.strInstructions, // Set the instruction data
                            ApiRecipeId = apiRecipe.idMeal
                        };
                    }
                    else
                    {
                        // Handle the case where API data is empty or not as expected
                        return NotFound();
                    }
                }
                else
                {
                    // Handle the case where API data is null or empty
                    return NotFound();
                }
            }

            return View("ApiDetails", recipe);
        }

        // GET: /Home/Favourite
        public IActionResult Favourite()
        {
            var favoriteRecipes = _dbContext.Recipes
                .Where(r => r.IsFavorite && !string.IsNullOrEmpty(r.ApiRecipeId))
                .ToList();

            return View(favoriteRecipes);
        }

        // POST: /Home/AddToFavorites/5
        public IActionResult AddToFavorites(int id)
        {
            var recipe = _dbContext.Recipes.FirstOrDefault(r => r.RecipeId == id);

            if (recipe == null)
            {
                return NotFound();
            }

            // Mark the recipe as a favorite and update the favorited date
            recipe.IsFavorite = true;
            recipe.DateFavourited = DateTime.Now;

            _dbContext.SaveChanges();

            // Reorder the list of recipes based on IsFavorite and DateFavourited
            var allRecipes = _dbContext.Recipes.ToList();
            allRecipes = allRecipes
                .OrderByDescending(r => r.IsFavorite)
                .ThenByDescending(r => r.DateFavourited)
                .ToList();

            // Redirect to the Index action to display the updated list of recipes
            return RedirectToAction("Index", new { searchQuery = "" });
        }

        // POST: /Home/AddApiRecipeToFavorites/abc123
        public async Task<IActionResult> AddApiRecipeToFavorites(string id)
        {
            // Fetch the API recipe from the API response
            string apiRecipeData = await _apiService.GetRecipeByIdAsync(id);
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(apiRecipeData);

            if (apiResponse.Meals != null && apiResponse.Meals.Count > 0)
            {
                var apiRecipe = apiResponse.Meals[0];

                // Create a new FavoriteApiRecipe object and save it to the database
                var favoriteApiRecipe = new Favourite
                {
                    ApiRecipeId = apiRecipe.idMeal,
                    RecipeName = apiRecipe.StrMeal,
                    RecipeCategory = apiRecipe.strCategory,
                    RecipeImage = apiRecipe.StrMealThumb,
                    RecipeInstruction = apiRecipe.strInstructions,
                    RecipeArea = apiRecipe.strArea,
                    DateFavourited = DateTime.Now
                };

                _dbContext.Favourites.Add(favoriteApiRecipe);
                _dbContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // POST: /Home/RemoveFromFavorites/5
        public IActionResult RemoveFromFavorites(int id)
        {
            var recipe = _dbContext.Recipes.FirstOrDefault(r => r.RecipeId == id);

            if (recipe == null)
            {
                return NotFound();
            }

            recipe.IsFavorite = false;
            _dbContext.SaveChanges();

            return RedirectToAction("Index"); // Redirect
        }

        // POST: /Home/RateRecipe/5
        [HttpPost]
        public IActionResult RateRecipe(int id, int rating)
        {
            var recipe = _dbContext.Recipes.FirstOrDefault(r => r.RecipeId == id);

            if (recipe == null)
            {
                return NotFound();
            }

            // Update rating and number of ratings
            recipe.Rating = (recipe.Rating * recipe.NumberOfRatings + rating) / (recipe.NumberOfRatings + 1);
            recipe.NumberOfRatings++;

            _dbContext.SaveChanges();

            return RedirectToAction("Index", new { id });
        }

        // GET: /Home/Error
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Display the Error view with details about the error
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
