using Microsoft.AspNetCore.Mvc;
using Recipe.WebApp.Models;
using Recipe.Library.Models;

namespace Recipe.WebApp.Controllers
{
    public class CreateController : Controller
    {
        // Define an instance of DbContext
        private readonly RecipeDbContext _dbContext; 

        public CreateController(RecipeDbContext dbContext)
        {
            // Inject the DbContext instance via constructor
            _dbContext = dbContext; 
        }

        // GET: /Create/Create
        [HttpGet]
        public IActionResult Create()
        {
            // Display the Create view when accessed via HTTP GET request
            return View("~/Views/Create/Create.cshtml");
        }

        // POST: /Create/CreateRecipe
        [HttpPost]
        public IActionResult CreateRecipe(RecipeItem recipe)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Add the recipe to the Recipes DbSet in the database context
                    _dbContext.Recipes.Add(recipe);
                    // Save changes to the database
                    _dbContext.SaveChanges();
                    // Redirect to the "Index" action
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

                // Handle the exception gracefully and provide user-friendly feedback
                return View("~/Views/Create/Create.cshtml", recipe);
            }
        }
    }
}
