using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Recipe.WebApp.Models;

namespace Recipe.WebApp.Tests
{
    [TestClass]
    public class DetailsTests
    {
        private IWebDriver _webdriver;
        private const string BaseUrl = "https://localhost:5001/";

        private DbContextOptions<RecipeDbContext>? _dbContextOptions;
        private RecipeDbContext? _dbContext;

        [TestInitialize]
        public void Setup()
        {
            // Set up
            _webdriver = new ChromeDriver();

            _dbContextOptions = new DbContextOptionsBuilder<RecipeDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

            _dbContext = new RecipeDbContext(_dbContextOptions);
        }

        [TestMethod]
        public void CheckDetailsPage()
        {
            // Navigate to Details page
            _webdriver?.Navigate().GoToUrl(BaseUrl + "Home/Details/2");

            // Use WebDriverWait to wait for the elements to become visible
            var wait = new WebDriverWait(_webdriver, TimeSpan.FromSeconds(10));

            try
            {
                // Wait for the recipe detail element to be visible
                var recipeDetailElement = wait.Until(driver =>
                    driver.FindElement(By.ClassName("recipe-detail")));

                // Verify recipe name
                var recipeNameElement = recipeDetailElement.FindElement(By.TagName("h1"));
                Assert.IsTrue(recipeNameElement.Text.Contains("Test Recipe"));
            }
            catch (WebDriverTimeoutException)
            {
                Assert.Fail("The recipe detail element was not found on the details page.");
            }
        }

        [TestMethod]
        public void CheckEditAndDeleteButtonsOnDetailsPage()
        {
            // Navigate to Details page
            _webdriver?.Navigate().GoToUrl(BaseUrl + "Home/Details/2");

            // Use WebDriverWait to wait for the elements to become visible
            var wait = new WebDriverWait(_webdriver, TimeSpan.FromSeconds(10));

            try
            {
                // Wait for the "Edit Recipe" link to be visible
                var editLink = wait.Until(driver =>
                    driver.FindElement(By.PartialLinkText("Edit Recipe")));

                // Wait for the "Delete Recipe" form to be visible
                var deleteForm = wait.Until(driver =>
                    driver.FindElement(By.CssSelector("form[action$='/Details/Delete/2']")));

                // Assert that both elements are present
                Assert.IsNotNull(editLink);
                Assert.IsNotNull(deleteForm);
            }
            catch (WebDriverTimeoutException)
            {
                Assert.Fail("The 'Edit Recipe' link or 'Delete Recipe' form was not found on the details page.");
            }
        }


        [TestCleanup]
        public void Teardown()
        {
            // Tear down
            _webdriver.Quit();

            // Roll back the database transaction
            _dbContext.Dispose();
        }
    }
}