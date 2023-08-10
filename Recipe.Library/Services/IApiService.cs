namespace Recipe.Library.Services
{
	public interface IApiService
	{
        Task<string> GetRandomMealAsync();
    }
}

