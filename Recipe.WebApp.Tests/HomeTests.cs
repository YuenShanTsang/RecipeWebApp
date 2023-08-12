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


        [TestCleanup]
        public void Teardown()
        {
            // Tear down
            _webdriver?.Quit();
        }
    }
}
