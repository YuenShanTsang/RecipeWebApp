﻿namespace Recipe.Library.Services
{
    // A service for making API requests to retrieve recipe data
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Gets a random meal from the API
        public async Task<string> GetRandomMealAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://www.themealdb.com/api/json/v1/1/random.php");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            throw new HttpRequestException($"API request failed with status code {response.StatusCode}");
        }

        // Gets a recipe by its ID from the API.
        public async Task<string> GetRecipeByIdAsync(string recipeId)
        {
            string apiUrl = $"https://www.themealdb.com/api/json/v1/1/lookup.php?i={recipeId}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    // Handle error cases here, such as returning null or an empty string
                    return string.Empty; 
                }
            }
        }


    }
}

