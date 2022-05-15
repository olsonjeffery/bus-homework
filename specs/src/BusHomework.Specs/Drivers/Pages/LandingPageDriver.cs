using OpenQA.Selenium;

namespace BusHomework.Specs.Drivers.Pages
{
  public class LandingPageDriver
  {
    private readonly PageDriver _D;
    private readonly UrlDriver _url;

    private By _byPageTitle = By.TagName("title");

    public LandingPageDriver(PageDriver page, UrlDriver url)
    {
      _D = page;
      _url = url;
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