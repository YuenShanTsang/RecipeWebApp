using Newtonsoft.Json;

namespace Recipe.Library.Models
{
    public class ApiResponse
    {
        [JsonProperty("Meals")]
        public List<Meal> Meals { get; set; }

        public ApiResponse()
        {
            Meals = new List<Meal>();
        }
    }

    public class Meal
    {
        public string idMeal { get; set; } = "";
        public string StrMeal { get; set; } = "";
        public string StrMealThumb { get; set; } = "";
        public string strCategory { get; set; } = "";
        public string strArea { get; set; } = "";
        public string strInstructions { get; set; } = "";
    }
}

