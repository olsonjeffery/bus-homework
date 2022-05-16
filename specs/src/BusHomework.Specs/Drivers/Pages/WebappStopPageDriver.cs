using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace BusHomework.Specs.Drivers.Pages
{
  public class WebappStopPageDriver
  {
    private readonly PageDriver _D;

    private readonly By _byStopDisplayClass = By.ClassName("stop-display-container");
    private readonly By _byShowingStopHiddenInput = By.CssSelector("input.visible-stop");

    public WebappStopPageDriver(PageDriver page)
    {
      _D = page;
    }

    public void WaitForSectionToLoad()
    {
      _D.WaitForAppearanceOf(_byStopDisplayClass);
    }

    internal IEnumerable<int> GetVisibleStopIds()
    {
      var visibleStopIds = new List<int>();
      var elems = _D.Driver.FindElements(_byShowingStopHiddenInput);
      foreach(var elem in elems)
      {
        var result = int.Parse(elem.GetAttribute("value"));
        visibleStopIds.Add(result);
      }

      return visibleStopIds;
    }
  }
}