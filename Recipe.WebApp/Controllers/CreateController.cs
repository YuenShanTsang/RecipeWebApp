using Microsoft.AspNetCore.Mvc;

namespace Recipe.WebApp.Controllers
{
    public class CreateController : Controller
    {
        // GET: /Create/Create
        [HttpGet]
        public IActionResult Create()
        {
            // Display the Create view when accessed via HTTP GET request
            return View("~/Views/Create/Create.cshtml");
        }

        // POST: /Create/CreateRecipe
        [HttpPost]
        public IActionResult CreateRecipe(Library.Models.RecipeItem recipe)
        {
            // This method handles the submission of the CreateRecipe form

            // TODO: Process the submitted recipe data (e.g., save to a database)

            // Redirect to the Index action of a different controller (replace "Index" with the actual action)
            return RedirectToAction("Index");
        }
    }
}
