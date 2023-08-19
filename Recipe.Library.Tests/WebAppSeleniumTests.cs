using Moq;
using Moq.Protected;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Recipe.Library.Services;
using System.Net;



namespace Recipe.WebApp.Tests
{
    [TestClass]
    public class WebAppSeleniumTests
    {
        private IWebDriver _driver;
        private const string BaseUrl = "https://localhost:5001/";

        [TestInitialize]
        public void Setup()
        {
            // Set up ChromeDriver
            _driver = new ChromeDriver();
        }

        [TestMethod]
        public void FavouritePageDisplaysRecipes()
        {
            // Arrange
            _driver.Navigate().GoToUrl(BaseUrl + "Favourite");

            // Wait for the recipe cards to be visible
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            var recipeCards = wait.Until(driver =>
                driver.FindElements(By.CssSelector(".card")));

            // Assert
            Assert.IsTrue(recipeCards.Count > 0, "No recipe cards found on the Favourite page.");
        }

        [TestMethod]
        public void ApiService_GetRandomMeal_ShouldReturnApiResponse()
        {
            // Arrange
            var mockHttpHandler = new Mock<HttpMessageHandler>();
            mockHttpHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{\"meal\": {...}}") // Replace with JSON response
                });

            var httpClient = new HttpClient(mockHttpHandler.Object);
            var apiService = new ApiService(httpClient);

            // Act
            var randomMeal = apiService.GetRandomMealAsync().Result;

            // Assert
            Assert.IsNotNull(randomMeal);
            // Add more assertions based on your expected response
        }


        [TestCleanup]
        public void Teardown()
        {
            // Tear down
            _driver.Quit();
        }
    }
}
