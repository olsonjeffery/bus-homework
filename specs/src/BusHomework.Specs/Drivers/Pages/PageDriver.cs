using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace BusHomework.Specs.Drivers.Pages
{
  public class PageDriver
  {
    private IWebDriver? _webDriver;
    public IWebDriver Driver
    {
      get
      {
        if (_webDriver == null)
        {
          _webDriver = new RemoteWebDriver(new Uri("http://localhost:4444"), new FirefoxOptions());
          _ctx["WebDriver"] = _webDriver;
        }
        return _webDriver;
      }
    }
    private readonly ScenarioContext _ctx;

    public PageDriver(ScenarioContext ctx)
    {
      _ctx = ctx;
    }

    internal void WaitForAppearanceOf(By elem)
    {
      new WebDriverWait(Driver, Constants.StandardWaitTimeout).Until<bool>((d) =>
      {
        return HasElement(elem);
      });
    }

    public bool HasElement(By selector)
    {
      try
      {
        Driver.FindElement(selector);
      }
      catch (ElementNotVisibleException)
      {
        return false;
      }
      return true;
    }

    public void TakeScreenshot(string feature, string scenario)
    {
      var result = ((ITakesScreenshot)Driver).GetScreenshot();
    }
  }
}