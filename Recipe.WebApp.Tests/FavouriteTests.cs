using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace Recipe.WebApp.Tests
{
    [TestClass]
    public class FavouriteTests
	{
        private IWebDriver? _webdriver;
        private const string BaseUrl = "https://localhost:5001/";

        [TestInitialize]
        public void Setup()
        {
            // Set up
            new DriverManager().SetUpDriver(new ChromeConfig());
            _webdriver = new ChromeDriver();
        }


        [TestMethod]
        public void FavouritePageDisplaysRecipes()
        {
            // Navigate to the Favourite page
            _webdriver.Navigate().GoToUrl(BaseUrl + "Favourite");

            // Use WebDriverWait to wait for the recipes to become visible
            var wait = new WebDriverWait(_webdriver, TimeSpan.FromSeconds(10));

            try
            {
                // Wait for the recipe cards to be visible
                var recipeCards = wait.Until(driver =>
                    driver.FindElements(By.CssSelector(".card")));

                // Assert that there are some recipe cards displayed
                Assert.IsTrue(recipeCards.Count > 0, "No recipe cards found on the Favourite page.");
            }
            catch (WebDriverTimeoutException)
            {
                Assert.Fail("Recipe cards were not found on the Favourite page.");
            }
        }

        [TestMethod]
        public void SortRecipes_ShouldSortRecipesCorrectly()
        {
            // Arrange
            _webdriver.Navigate().GoToUrl(BaseUrl + "Favourite/Favourite");

            // Wait for the "Sort by" dropdown to be present
            var wait = new WebDriverWait(_webdriver, TimeSpan.FromSeconds(10));
            var sortByDropdown = wait.Until(driver =>
                driver.FindElement(By.Id("sortBy")));

            // Select the "Rating" sorting option
            var selectElement = new SelectElement(sortByDropdown);
            selectElement.SelectByText("Rating");

            // Wait for the recipes to be reloaded with the new sorting option
            wait.Until(driver =>
                driver.FindElements(By.CssSelector(".card")));

            // Get the list of recipe ratings
            var recipeRatings = _webdriver.FindElements(By.CssSelector(".card-text.recipe-rating"))
                                           .Select(element => element.Text)
                                           .ToList();

            // Convert the ratings to floats for comparison
            var floatRecipeRatings = recipeRatings.Select(float.Parse).ToList();

            // Assert: Check if the recipes are sorted in descending order of ratings
            var isSorted = floatRecipeRatings.SequenceEqual(floatRecipeRatings.OrderByDescending(r => r));
            Assert.IsTrue(isSorted, "Recipes are not sorted by rating in descending order.");
        }


        [TestCleanup]
        public void Teardown()
        {
            // Tear down
            _webdriver?.Quit();
        }

    }
}

