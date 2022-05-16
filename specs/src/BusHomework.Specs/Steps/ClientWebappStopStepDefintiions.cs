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
  public class ClientWebappStopStepDefinitions
  {
    private readonly ScenarioContext _ctx;
    private readonly LandingPageDriver _landingPage;

    public ClientWebappStopStepDefinitions(ScenarioContext ctx, LandingPageDriver landingPage)
    {
      _ctx = ctx;
      _landingPage = landingPage;
    }

    [Then("Stop details for Stop (.*) should be showing")]
    public void ThenStopDetailsForStopShouldBeShowing(int stopId)
    {
      var visibleStops = _landingPage.Stop.GetVisibleStopIds();
      var result = visibleStops.Where(x=>x == stopId);
      Assert.AreEqual(1, result.Count());
    }

    [When("the Stops section is loaded and available")]
    public void WhenTheStopSectionIsLoadedAndAvailable()
    {
      _landingPage.Stop.WaitForSectionToLoad();
    }
  }
}