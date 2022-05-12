using BusHomework.Specs.Drivers;
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
    }

    [Given("a call to fetch arrivals at stop (.*)")]
    public void ACallToFetchArrivalsAt(int stopId)
    {
      _scenarioContext[Constants.StopIdKey] = stopId;
    }

    [When("calling at \"(.*)\" for Stop (.*)")]
    public void WhenCallingAt(string callTime, int stopId)
    {
      _scenarioContext[Constants.StopIdKey] = stopId;
      WhenCallingAt(callTime);
    }

    [When("calling at \"(.*)\"")]
    public void WhenCallingAt(string callTime)
    {
      var stopId = _scenarioContext[Constants.StopIdKey];
      _scenarioContext[Constants.CallTimeKey] = callTime;
      var results = _stop.GetUpcomingArrivalsAt(stopId, callTime);
    }

    [Then("the Stop endpoint should return exactly two upcoming arrival results")]
    public void ThenTheStopEndpointShouldReturnExactlyTwoUpcomingArrivalResults()
    {
      _scenarioContext.Pending();
    }

    [Then("the (.*) should arrive at (.*) and the (.*) should arrive at (.*)")]
    public void ThenTheShouldArriveAtAndTheShouldArriveAt(int nextRouteId, string nextTime, int secondRouteId, string secondTime)
    {
      _scenarioContext.Pending();
    }
  }
}
