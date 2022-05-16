using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusHomework.Specs.Drivers;
using BusHomework.Specs.Drivers.Pages;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace BusHomework.Specs.Steps
{
  [Binding]
  public class ClientWebappStepDefinitions
  {
    private readonly ScenarioContext _ctx;
    private readonly LandingPageDriver _landingPage;

    public ClientWebappStepDefinitions(ScenarioContext ctx, LandingPageDriver landingPage)
    {
      _ctx = ctx;
      _landingPage = landingPage;
    }

    [Given("a visitor navigates to the landing page")]
    public void GivenAVisitorNavigatesToTheLandingPage()
    {
      _landingPage.NavigateTo();
    }

    [Then("the AppBar should contain an \"(.*)\" section")]
    [Then("the AppBar should contain a \"(.*)\" section")]
    public void ThenTheAppBarShouldContainASection(string expectedTitle)
    {
      Assert.AreEqual(1, _landingPage.AppBar.GetAppBarSections().Where(x=>x.ToLowerInvariant() == expectedTitle.ToLowerInvariant()).Count());
    }

    [Then("the page title should be \"(.*)\"")]
    public void ThenThePageTitleShouldBe(string expectedTitle)
    {
      var actualTitle = _landingPage.GetPageTitle();

      Assert.AreEqual(expectedTitle, actualTitle);
    }

    [Then("the selected AppBar entry should be \"(.*)\"")]
    public void ThenTheSelectedAppBarEntryShouldBe(string expectedSelectedTab)
    {
      var actualSelectedTab = _landingPage.AppBar.GetSelectedAppBarSection();

      Assert.AreEqual(expectedSelectedTab.ToLowerInvariant(), actualSelectedTab.ToLowerInvariant());
    }
  }
}