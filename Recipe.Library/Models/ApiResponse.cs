using Newtonsoft.Json;

namespace Recipe.Library.Models
{
    // The response structure from the API
    public class ApiResponse
    {
        // Gets or sets the list of meals retrieved from the API response
        [JsonProperty("Meals")]
        public List<Meal> Meals { get; set; }

        // Initializes a new instance
        public ApiResponse()
        {
            Meals = new List<Meal>();
        }
    }

    // A meal retrieved from the API response.
    public class Meal
    {
        // ID of the meal
        public string idMeal { get; set; } = "";

        // Name of the meal
        public string StrMeal { get; set; } = "";

        // URL of the meal's thumbnail image
        public string StrMealThumb { get; set; } = "";

        // Category of the meal
        public string strCategory { get; set; } = "";

        // Area or Country associated with the meal
        public string strArea { get; set; } = "";

        // Instructions for preparing the meal
        public string strInstructions { get; set; } = "";
    }
}

