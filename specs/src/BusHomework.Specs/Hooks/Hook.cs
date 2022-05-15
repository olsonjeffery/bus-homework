using System;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace BusHomework.Specs.Hooks
{
  [Binding]
  public class Hooks
  {
    [AfterScenario]
    public static void AfterScenario(ScenarioContext ctx)
    {
      // for selenium connection
      if (ctx.ContainsKey("WebDriver"))
      {
        ((IWebDriver)ctx["WebDriver"]).Quit();
      }
    }
  }
}
