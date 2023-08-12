using Microsoft.AspNetCore.Mvc;
using Recipe.Library.Models;
using Recipe.WebApp.Models;

namespace Recipe.WebApp.Controllers
{
    public class DetailsController : Controller
    {
        private readonly RecipeDbContext _dbContext;

        public DetailsController(RecipeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: /Details/Edit/5
        public IActionResult Edit(int id)
        {
            var recipe = _dbContext.Recipes.Find(id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View("~/Views/Edit/Edit.cshtml", recipe);
        }

        // POST: /Details/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, RecipeItem model)
        {
            if (id != model.RecipeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _dbContext.Update(model);
                _dbContext.SaveChanges();
                return Redirect("~/Home/Index");
            }

            return View(model);
        }

        // POST: /Details/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var recipe = _dbContext.Recipes.Find(id);
            if (recipe == null)
            {
                return NotFound();
            }

            _dbContext.Recipes.Remove(recipe);
            _dbContext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}
