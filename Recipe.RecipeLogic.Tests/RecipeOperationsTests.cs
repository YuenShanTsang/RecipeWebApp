namespace Recipe.RecipeLogic.Tests;

[TestClass]
public class RecipeOperationsTests
{
    [TestMethod]
    public void GetRecipeName_ValidId_ReturnsRecipeName()
    {
        // Arrange
        var recipeOperations = new RecipeOperations();
        var recipeId = "123";

        // Act
        var recipeName = recipeOperations.GetRecipeName(recipeId);

        // Assert
        Assert.AreEqual("Sample Recipe", recipeName);
    }
}
