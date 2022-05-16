using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace BusHomework.Specs.Drivers.Pages
{
  public class LandingPageDriver
  {
    public WebappAppBarPageDriver AppBar { get { return _appBar; } }
    public WebappStopPageDriver Stop { get { return _stop; } }

    private readonly PageDriver _D;
    private readonly UrlDriver _url;
    private readonly WebappAppBarPageDriver _appBar;
    private readonly WebappStopPageDriver _stop;

    private By _byPageTitle = By.TagName("title");

    public LandingPageDriver(PageDriver page, UrlDriver url, WebappAppBarPageDriver appBar, WebappStopPageDriver stop)
    {
      _D = page;
      _url = url;
      _appBar = appBar;
      _stop = stop;
    }

    public void NavigateTo()
    {
      var landingPageUrl = _url.GetWebappUrl("/");
      _D.Driver.Navigate().GoToUrl(landingPageUrl);
      WaitForPageToLoad();
    }

    public void WaitForPageToLoad()
    {
      _D.WaitForAppearanceOf(_byPageTitle);
    }

    public string GetPageTitle()
    {
      return _D.Driver.Title;
    }
  }
}