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

    [Then("the Stop endpoint should return exactly two upcoming arrival results")]
    public void ThenTheStopEndpointShouldReturnExactlyTwoUpcomingArrivalResults()
    {
      var results = (StopEndpointResult)_scenarioContext[Constants.UpcomingArrivalsResultKey];
      Assert.AreEqual(2, results.UpcomingArrivals.Count());
    }

    [Then("the (.*) should arrive at (.*) and the (.*) should arrive at (.*)")]
    public void ThenTheShouldArriveAtAndTheShouldArriveAt(int nextRouteId, string nextTime, int secondRouteId, string secondTime)
    {
      var results = (StopEndpointResult)_scenarioContext[Constants.UpcomingArrivalsResultKey];

      Assert.AreEqual(nextRouteId, results.UpcomingArrivals.First().RouteId);
      Assert.AreEqual(nextTime, results.UpcomingArrivals.First().ArrivalTime.Split("T")[1]);

      Assert.AreEqual(secondRouteId, results.UpcomingArrivals.ElementAt(1).RouteId);
      Assert.AreEqual(secondTime, results.UpcomingArrivals.ElementAt(1).ArrivalTime.Split("T")[1]);
    }

    [Then("the Call Time should be \"(.*)\"")]
    public void ThenTheCallTimeShouldBe(string callTime)
    {
      var results = (StopEndpointResult)_scenarioContext[Constants.UpcomingArrivalsResultKey];
      Assert.AreEqual(callTime, results.CallTimestamp.Split("T")[1]);
    }
  }
}
