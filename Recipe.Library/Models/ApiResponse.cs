namespace Recipe.Library.Models
{
    public class ApiResponse
    {
        public List<Meal> Meals { get; set; }
    }

    public class Meal
    {
        public string idMeal { get; set; }
        public string StrMeal { get; set; }
        public string StrMealThumb { get; set; }
        public string strCategory { get; set; }
        public string strArea { get; set; }
        public string strInstructions { get; set; }
    }
}

