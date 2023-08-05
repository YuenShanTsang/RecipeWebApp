using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Recipe.WebApp.Controllers
{
    public class CreateController : Controller
    {
        [HttpGet]
        public IActionResult Create()
        {
            return View("~/Views/Create/Create.cshtml");
        }


        [HttpPost]
        public IActionResult CreateRecipe(Models.RecipeItem recipe)
        {

            return RedirectToAction("Index");
        }

    }
}

