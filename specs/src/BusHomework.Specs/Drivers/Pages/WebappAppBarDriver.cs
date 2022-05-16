using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace BusHomework.Specs.Drivers.Pages
{
  public class WebappAppBarDriver
  {
    private readonly PageDriver _D;

    private By _byMuiTab = By.ClassName("MuiTab-fullWidth");

    public WebappAppBarDriver(PageDriver page)
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
  }
}