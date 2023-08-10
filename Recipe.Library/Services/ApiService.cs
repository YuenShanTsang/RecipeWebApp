namespace Recipe.Library.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetRandomMealAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://www.themealdb.com/api/json/v1/1/random.php");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            throw new HttpRequestException($"API request failed with status code {response.StatusCode}");
        }
    }
}

