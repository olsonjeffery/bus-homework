using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusHomework.Specs.Drivers;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace BusHomework.Specs.Steps
{
  [Binding]
  public sealed class StopStepDefinitions
  {

    // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

    private readonly ScenarioContext _scenarioContext;
    private readonly StopDriver _stop;

    public StopStepDefinitions(ScenarioContext scenarioContext, StopDriver stop)
    {
      _scenarioContext = scenarioContext;
      _stop = stop;
    }

    [Given("an endpoint for fetching info about a Stop")]
    public void GivenAnEndpointForFetchingInfoAboutAStop()
    {
      // NOOP
    }

    [Given("a call to fetch arrivals at stop (.*)")]
    public void ACallToFetchArrivalsAt(int stopId)
    {
      _scenarioContext[Constants.StopIdKey] = stopId;
    }

    [When("calling at \"(.*)\" for Stop (.*)")]
    public async Task WhenCallingAt(string callTime, int stopId)
    {
      _scenarioContext[Constants.StopIdKey] = stopId;
      await WhenCallingAt(callTime);
    }

    [When("calling for Stop (.*)")]
    public async Task WhenCallingForStop(int stopId)
    {
      var results = await _stop.GetUpcomingArrivalsFor(stopId);
      _scenarioContext[Constants.UpcomingArrivalsResultKey] = results;
    }

    [When("calling at \"(.*)\"")]
    public async Task WhenCallingAt(string callTime)
    {
      var stopId = (int)_scenarioContext[Constants.StopIdKey];
      _scenarioContext[Constants.CallTimeKey] = callTime;
      await _stop.SetTime(callTime);
      await WhenCallingForStop(stopId);
      await _stop.UnsetTime();
    }

    [Then("the Stop endpoint should return exactly two upcoming arrival results")]
    public void ThenTheStopEndpointShouldReturnExactlyTwoUpcomingArrivalResults()
    {
      var results = (IEnumerable<UpcomingArrival>)_scenarioContext[Constants.UpcomingArrivalsResultKey];
      Assert.AreEqual(2, results.Count());
    }

    [Then("the (.*) should arrive at (.*) and the (.*) should arrive at (.*)")]
    public void ThenTheShouldArriveAtAndTheShouldArriveAt(int nextRouteId, string nextTime, int secondRouteId, string secondTime)
    {
      _scenarioContext.Pending();
    }
  }
}
