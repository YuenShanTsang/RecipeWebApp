using Newtonsoft.Json;
using Recipe.Library.Models;

[TestClass]
public class RecipeItemTests
{
    [TestMethod]
    public void RecipeItem_SerializationDeserialization_ShouldBeValid()
    {
        // Arrange
        RecipeItem original = new RecipeItem
        {
            RecipeName = "Recipe 1",
            RecipeCategory = "Beef"
        };

        // Act
        string serialized = JsonConvert.SerializeObject(original);
        RecipeItem deserialized = JsonConvert.DeserializeObject<RecipeItem>(serialized);

        // Assert
        Assert.AreEqual(original.RecipeName, deserialized.RecipeName);
        Assert.AreEqual(original.RecipeCategory, deserialized.RecipeCategory);
        // Verify other properties as needed
    }
}
