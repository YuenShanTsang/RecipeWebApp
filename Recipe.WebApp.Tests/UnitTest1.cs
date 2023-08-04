using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace Recipe.WebApp.Tests;

[TestClass]
public class UnitTest1
{
    private IWebDriver? _webdriver;
    private const string BaseUrl = "https://localhost:5001/";

    [TestInitialize]
    public void Setup()
    {
        // Set up
        new DriverManager().SetUpDriver(new ChromeConfig());
        _webdriver = new ChromeDriver();
    }

    [TestMethod]
    public void EnsureHomePageHasHomeInTheTitle()
    {
        _webdriver?.Navigate().GoToUrl(BaseUrl);
        Assert.IsTrue(_webdriver?.Title.Contains("Home"));
    }

    [TestCleanup]
    public void Teardown()
    {
        //Tear down
        _webdriver?.Quit();
    }
}
