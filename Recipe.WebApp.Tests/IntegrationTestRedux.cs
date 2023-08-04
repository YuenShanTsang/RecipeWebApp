
using Microsoft.AspNetCore.Mvc;
using Recipe.WebApp.Controllers;

namespace Recipe.WebApp.Tests
{
    [TestClass]
    public class IntegrationTestRedux
	{
        [TestMethod]
        public async void Method1()
        {
            var home = new HomeController();
            var result = home.Index();

            var ctx = new ActionContext();

            if (result != null)
            {
                await result.ExecuteResultAsync(ctx);
                var bdy = home.Response.Body.ToString;

                int x = 5;
            }
                

        }
    }
}

