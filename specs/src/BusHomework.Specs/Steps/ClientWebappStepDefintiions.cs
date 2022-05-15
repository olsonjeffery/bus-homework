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

    [Then("the page title should be \"(.*)\"")]
    public void ThenThePageTitleShouldBe(string expectedTitle)
    {
      var actualTitle = _landingPage.GetPageTitle();

      Assert.AreEqual(expectedTitle, actualTitle);
    }
  }
}