using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Recipe.Library.Models;
using Recipe.WebApp.Models;

namespace Recipe.WebApp.Controllers
{
    public class HomeController : Controller
    {
        // A static list to store recipe items
        private static List<RecipeItem> _recipes = new List<RecipeItem>();

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // GET: /Home/Index
        public IActionResult Index()
        {
            // Display the list of recipes on the Index view
            return View(_recipes);
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
