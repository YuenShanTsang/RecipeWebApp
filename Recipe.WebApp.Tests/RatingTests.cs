using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using System;
using System.Linq;

namespace Recipe.WebApp.Tests
{
    [TestClass]
    public class RatingTests
    {
        private IWebDriver _webDriver;
        private const string BaseUrl = "https://localhost:5001/";

        [TestInitialize]
        public void Setup()
        {
            // Set up
            new DriverManager().SetUpDriver(new ChromeConfig());
            _webDriver = new ChromeDriver();
        }

        [TestMethod]
        public void RateRecipe_ShouldUpdateRecipeRating()
        {
            // Arrange
            _webDriver.Navigate().GoToUrl(BaseUrl);

            // Find a recipe card
            var recipeCard = _webDriver.FindElement(By.Id("rating-form"));

            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));

            // Wait for the rating display element using XPath
            var initialRatingElement = wait.Until(driver =>
                driver.FindElement(By.XPath("//*[@id='rating-display']")));

            // Get the initial recipe rating and number of ratings
            var initialRatingText = initialRatingElement.Text;
            var initialRatingValue = double.Parse(initialRatingText.Split(':')[1].Trim().Split(' ')[0]);

            // Find the rating input field
            var ratingInput = wait.Until(driver =>
                recipeCard.FindElement(By.XPath(".//input[@id='rating-input']")));

            var submitButton = wait.Until(driver =>
                recipeCard.FindElement(By.XPath(".//button[@id='rating-btn']")));

            // Set the new rating value
            var newRating = 4;
            ratingInput.Clear();
            ratingInput.SendKeys(newRating.ToString());

            // Act
            submitButton.Click();

            // Refresh the page to ensure the DOM is up-to-date
            _webDriver.Navigate().Refresh();

            // Re-find the recipe card
            recipeCard = _webDriver.FindElement(By.Id("rating-form"));

            // Get the updated recipe rating and number of ratings
            var updatedRatingText = recipeCard.FindElement(By.XPath("//*[@id='rating-display']")).Text;
            var updatedRatingValue = double.Parse(updatedRatingText.Split(':')[1].Trim().Split(' ')[0]);

            // Assert
            Assert.AreEqual(newRating, updatedRatingValue, "Recipe rating was not updated.");

        }

        [TestCleanup]
        public void Teardown()
        {
            // Tear down
            _webDriver.Quit();
        }
    }
}
