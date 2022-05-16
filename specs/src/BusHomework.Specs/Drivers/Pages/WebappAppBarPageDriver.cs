using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace BusHomework.Specs.Drivers.Pages
{
  public class WebappAppBarPageDriver
  {
    private readonly PageDriver _D;

    private By _byMuiTab = By.ClassName("MuiTab-fullWidth");
    private By _bySelectedMuiTab = By.CssSelector(".MuiTab-fullWidth.Mui-selected");

    public WebappAppBarPageDriver(PageDriver page)
    {
      _D = page;
    }

    public IEnumerable<string> GetAppBarSections()
    {
      var results = _D.Driver.FindElements(_byMuiTab);
      var sections = new List<string>();
      foreach(var tab in results)
      {
        sections.Add(tab.Text);
      }
      return sections;
    }

    public string GetSelectedAppBarSection()
    {
      return _D.Driver.FindElement(_bySelectedMuiTab).Text;
    }
  }
}