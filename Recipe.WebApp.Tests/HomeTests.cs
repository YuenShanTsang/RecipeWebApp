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
        public void CreateRecipeAndCheckOnHomePage()
        {
            // Navigate to Create page
            _webdriver?.Navigate().GoToUrl(BaseUrl + "Create");

            // Use WebDriverWait to wait for the element to become visible
            var wait = new WebDriverWait(_webdriver, TimeSpan.FromSeconds(10));
            var recipeNameField = wait.Until(driver =>
                driver.FindElement(By.Id("RecipeName")));

            // Fill out the form fields
            recipeNameField.SendKeys("Test Recipe");

            // Find the submit button
            var submitButton = _webdriver?.FindElement(By.CssSelector("form button[type='submit']"));

            // Click using JavaScript to bypass any intercepting elements
            ((IJavaScriptExecutor)_webdriver).ExecuteScript("arguments[0].click();", submitButton);

            // Navigate to Home page
            _webdriver?.Navigate().GoToUrl(BaseUrl);

            // Check if the created recipe name is displayed on the Home page
            Assert.IsTrue(_webdriver?.PageSource.Contains("Test Recipe"));
        }


        [TestMethod]
        public void CheckDetailsPage()
        {
            // Navigate to Details page
            _webdriver?.Navigate().GoToUrl(BaseUrl + "Home/Details/1");

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

        [TestCleanup]
        public void Teardown()
        {
            // Tear down
            _webdriver?.Quit();
        }
    }
}
