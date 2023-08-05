using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Recipe.WebApp.Models;

namespace Recipe.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private static List<RecipeItem> _recipes = new List<RecipeItem>();

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(_recipes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("CreateRecipe")]
        public IActionResult CreateRecipe(RecipeItem recipe)
        {
            _recipes.Add(recipe);

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
