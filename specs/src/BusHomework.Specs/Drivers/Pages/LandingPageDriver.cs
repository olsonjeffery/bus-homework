using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace BusHomework.Specs.Drivers.Pages
{
  public class LandingPageDriver
  {
    public WebappAppBarDriver AppBar { get { return _appBar; } }

    private readonly PageDriver _D;
    private readonly UrlDriver _url;
    private readonly WebappAppBarDriver _appBar;

    private By _byPageTitle = By.TagName("title");

    public LandingPageDriver(PageDriver page, UrlDriver url, WebappAppBarDriver appBar)
    {
      _D = page;
      _url = url;
      _appBar = appBar;
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