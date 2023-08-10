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
        // A static list to store recipe items
        private static List<RecipeItem> _recipes = new List<RecipeItem>();

        private readonly IApiService _apiService;

        public HomeController(IApiService apiService)
        {
            _apiService = apiService;
        }

        // GET: /Home/Index
        public async Task<IActionResult> Index()
        {
            // Create a list to store recipes
            List<RecipeItem> recipes = new List<RecipeItem>();

            // Fetch 50 random meal data from the API
            for (int i = 0; i < 50; i++)
            {
                string randomMealData = await _apiService.GetRandomMealAsync();

                // Deserialize the API response
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(randomMealData);

                // Add the recipe to the list
                if (apiResponse.Meals.Count > 0)
                {
                    recipes.Add(new RecipeItem { RecipeName = apiResponse.Meals[0].StrMeal });
                }
            }

            return View(recipes);
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
            // Add the submitted recipe to the list
            _recipes.Add(recipe);

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
