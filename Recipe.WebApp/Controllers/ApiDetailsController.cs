//using System.Linq;
//using Microsoft.AspNetCore.Mvc;
//using Recipe.Library.Models;
//using Recipe.WebApp.Models;

//namespace Recipe.WebApp.Controllers
//{
//    public class ApiDetailsController : Controller
//    {
//        private readonly RecipeDbContext _dbContext;

//        public ApiDetailsController(RecipeDbContext dbContext)
//        {
//            _dbContext = dbContext;
//        }

//        // GET: /ApiDetails/Details/{id}
//        public IActionResult Details(string id)
//        {
//            // Retrieve the recipe details from the database based on the API recipe ID
//            var recipe = _dbContext.Recipes.FirstOrDefault(r => r.ApiRecipeId == id);

//            if (recipe == null)
//            {
//                return NotFound(); // Handle the case where the recipe is not found
//            }

//            return View("ApiDetails", recipe);
//        }
//    }
//}
