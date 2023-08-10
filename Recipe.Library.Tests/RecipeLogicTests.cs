namespace Recipe.Library.Tests
{
    [TestClass]
    public class RecipeOperationsTests
    {
        [TestMethod]
        public void GetRecipeName_ValidId_ReturnsRecipeName()
        {
            // Arrange
            var recipeLogic = new RecipeLogic();
            var recipeId = "123";

            // Act
            var recipeName = recipeLogic.GetRecipeName(recipeId);

            // Assert
            Assert.AreEqual("Sample Recipe", recipeName);
        }

    }
}
