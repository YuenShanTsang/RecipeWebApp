using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace Recipe.WebApp.Tests
{
    [TestClass]
    public class HomeTests
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
        public void HomePageDisplaysRecipes()
        {
            // Navigate to the Home page
            _webdriver.Navigate().GoToUrl(BaseUrl);

            // Use WebDriverWait to wait for the recipes to become visible
            var wait = new WebDriverWait(_webdriver, TimeSpan.FromSeconds(10));

            try
            {
                // Wait for the recipe cards to be visible
                var recipeCards = wait.Until(driver =>
                    driver.FindElements(By.CssSelector(".card")));

                // Assert that there are some recipe cards displayed
                Assert.IsTrue(recipeCards.Count > 0, "No recipe cards found on the home page.");
            }
            catch (WebDriverTimeoutException)
            {
                Assert.Fail("Recipe cards were not found on the home page.");
            }
        }

        [TestMethod]
        public void AddToFavorites_ButtonClick_ShouldMarkRecipeAsFavorite()
        {
            // Arrange
            _webdriver.Navigate().GoToUrl(BaseUrl);

            // Find a recipe card
            var recipeCard = _webdriver.FindElement(By.Id("fav-btn"));

            var wait = new WebDriverWait(_webdriver, TimeSpan.FromSeconds(5));

            // Find the "Add to Favorites" button within the specific recipe card
            var addToFavoritesButton = wait.Until(driver =>
                recipeCard.FindElement(By.XPath(".//button[@id='add-to-favorites']")));

            // Get the recipe card's text (or any attribute) before clicking the button
            var recipeCardTextBeforeClick = recipeCard.Text;

            // Act
            addToFavoritesButton.Click();

            // Refresh the page to ensure the DOM is up-to-date
            _webdriver.Navigate().Refresh();

            // Re-find the recipe card
            recipeCard = _webdriver.FindElement(By.Id("fav-btn"));

            // Find the "Remove from Favorites" button within the refreshed recipe card
            var removeFromFavoritesButton = recipeCard.FindElement(By.CssSelector("#remove-from-favorites"));

            // Assert
            var isFavoriteButtonDisplayed = removeFromFavoritesButton.Displayed;

            Assert.IsTrue(isFavoriteButtonDisplayed, "Recipe was not marked as favorite.");
        }

        [TestMethod]
        public void SearchFunction_ShouldFilterRecipes()
        {
            _webdriver.Navigate().GoToUrl(BaseUrl);

            var wait = new WebDriverWait(_webdriver, TimeSpan.FromSeconds(5));

            // Wait for the search query input element to be visible using XPath
            var searchInput = _webdriver.FindElement(By.Id("searchInput"));

            // Act
            string searchQuery = "Chicken";
            searchInput.SendKeys(searchQuery);

            // Find and click the search button
            var searchButton = _webdriver.FindElement(By.Id("search-button"));
            searchButton.Click();

            // Wait for the search results to be visible
            var recipeCards = wait.Until(driver =>
                driver.FindElements(By.CssSelector(".card")));

            // Assert: Check if search results are displayed
            Assert.IsTrue(recipeCards.Count > 0, "No recipe cards found in search results.");
        }

        [TestCleanup]
        public void Teardown()
        {
            // Tear down
            _webdriver?.Quit();
        }
    }
}
