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
    private readonly TimeDriver _time;

    public StopStepDefinitions(ScenarioContext scenarioContext, StopDriver stop, TimeDriver time)
    {
      _scenarioContext = scenarioContext;
      _stop = stop;
      _time = time;
    }

    [Given("an endpoint for fetching info about a Stop")]
    public void GivenAnEndpointForFetchingInfoAboutAStop()
    {
      // NOOP
    }

    [When("calling at \"(.*)\" for Stop (.*)")]
    public async Task WhenCallingAtForStop(string callTime, int stopId)
    {
      _scenarioContext[Constants.StopIdKey] = stopId;
      var results = await _stop.GetUpcomingArrivalsFor(stopId, callTime);
      _scenarioContext[Constants.UpcomingArrivalsResultKey] = results;
    }

    [When("calling for Stop (.*)")]
    public async Task WhenCallingForStop(int stopId)
    {
      var results = await _stop.GetUpcomingArrivalsFor(stopId);
      _scenarioContext[Constants.UpcomingArrivalsResultKey] = results;
    }

    [Then("the Stop endpoint should return exactly (.*) upcoming arrival results")]
    public void ThenTheStopEndpointShouldReturnExactlyTwoUpcomingArrivalResults(int resultsCount)
    {
      var results = (StopEndpointResult)_scenarioContext[Constants.UpcomingArrivalsResultKey];
      Assert.AreEqual(resultsCount, results.UpcomingArrivals.Count());
    }


    [Then("the (.*) should arrive at (.*) and at (.*)")]
    public void ThenTheShouldArriveAtAndTheShouldArriveAt(int routeId, string nextTime, string secondTime)
    {
      var results = (StopEndpointResult)_scenarioContext[Constants.UpcomingArrivalsResultKey];

      var matchingArrivals = results.UpcomingArrivals.Where(x=>x.RouteId == routeId).OrderBy(x=>x.ArrivalTime);

      Assert.AreEqual(routeId, matchingArrivals.First().RouteId);
      Assert.AreEqual(nextTime, matchingArrivals.First().ArrivalTime.Split("T")[1]);
      Assert.AreEqual(routeId, matchingArrivals.ElementAt(1).RouteId);
      Assert.AreEqual(secondTime, matchingArrivals.ElementAt(1).ArrivalTime.Split("T")[1]);
    }

    [Then("the Call Time should be \"(.*)\"")]
    public void ThenTheCallTimeShouldBe(string callTime)
    {
      var results = (StopEndpointResult)_scenarioContext[Constants.UpcomingArrivalsResultKey];
      Assert.AreEqual(callTime, results.CallTimestamp.Split("T")[1]);
    }
  }
}
