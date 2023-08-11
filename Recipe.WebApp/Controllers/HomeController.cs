using System.Diagnostics;
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
        public async Task<IActionResult> Index()
        {
            // Retrieve user-created recipes from the database
            List<RecipeItem> userRecipes = _dbContext.Recipes.ToList();

            // Retrieve API recipes
            List<RecipeItem> apiRecipes = new List<RecipeItem>();

            for (int i = 0; i < 30; i++)
            {
                string randomMealData = await _apiService.GetRandomMealAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(randomMealData);

                if (apiResponse.Meals.Count > 0)
                {
                    var apiRecipe = apiResponse.Meals[0];
                    apiRecipes.Add(new RecipeItem
                    {
                        RecipeName = apiRecipe.StrMeal,
                        RecipeCategory = apiRecipe.strCategory, // Set the category property
                        RecipeImage = apiRecipe.StrMealThumb // Set the image property
                    });
                }
            }


            // Combine user-created and API recipes into a single list
            List<RecipeItem> allRecipes = userRecipes.Concat(apiRecipes).ToList();

            return View(allRecipes);
        }

        public IActionResult Details(int id)
        {
            // Retrieve the recipe details from the database based on the ID
            var recipe = _dbContext.Recipes.FirstOrDefault(r => r.RecipeId == id);

            if (recipe == null)
            {
                return NotFound(); // Handle the case where the recipe is not found
            }

            return View(recipe); // Pass the recipe to the Detail view
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
            // Add the submitted recipe to the database
            _dbContext.Recipes.Add(recipe);
            _dbContext.SaveChanges();

            // Redirect to the Index action to display the updated list of recipes
            return RedirectToAction("Index");
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
