namespace Recipe.Library.Services
{
	public interface IApiService
	{
        Task<string> GetRandomMealAsync();
        Task<string> GetRecipeByIdAsync(string recipeId);
    }
}

