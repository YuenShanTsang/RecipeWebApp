using Microsoft.AspNetCore.Mvc;
using Recipe.WebApp.Models;
using Recipe.Library.Models;

namespace Recipe.WebApp.Controllers
{
    public class CreateController : Controller
    {
        private readonly RecipeDbContext _dbContext; // Define an instance of your DbContext

        public CreateController(RecipeDbContext dbContext)
        {
            _dbContext = dbContext; // Inject the DbContext instance via constructor
        }

        // GET: /Create/Create
        [HttpGet]
        public IActionResult Create()
        {
            // Display the Create view when accessed via HTTP GET request
            return View("~/Views/Create/Create.cshtml");
        }

        [HttpPost]
        public IActionResult CreateRecipe(RecipeItem recipe)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _dbContext.Recipes.Add(recipe);
                    _dbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    // Handle validation errors
                    return View("~/Views/Create/Create.cshtml", recipe);
                }
            }
            catch (Exception ex)
            {
                // Log or output the exception details
                Console.WriteLine(ex.ToString());

                // Optionally, handle the exception gracefully and provide user-friendly feedback
                return View("~/Views/Create/Create.cshtml", recipe);
            }
        }
    }
}
