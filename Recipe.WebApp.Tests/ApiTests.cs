namespace Recipe.WebApp.Tests
{
    [TestClass]
    public class ApiTests
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://localhost:5001/";

        public ApiTests()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(BaseUrl);
        }

        [TestMethod]
        public async Task GetNonExistingRecipe_ReturnsNotFound()
        {
            // Arrange
            int recipeId = 999; // This ID doesn't exist

            // Act
            var response = await _httpClient.GetAsync($"Home/ApiDetails/{recipeId}");

            // Assert
            Assert.AreEqual(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

    }
}
