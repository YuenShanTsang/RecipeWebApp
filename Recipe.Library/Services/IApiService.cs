namespace Recipe.Library.Services
{
    // A service for interacting with an external recipe API.
    public interface IApiService
	{
        Task<string> GetRandomMealAsync();
        Task<string> GetRecipeByIdAsync(string recipeId);
    }
}

