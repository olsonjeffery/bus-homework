using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusHomework.Specs.Drivers;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace BusHomework.Specs.Steps
{
  [Binding]
  public sealed class CommonStepDefinitions
  {

    // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

    private readonly ScenarioContext _scenarioContext;

    public CommonStepDefinitions(ScenarioContext scenarioContext)
    {
      _scenarioContext = scenarioContext;
    }

    [Given("scenario is pending")]
    public void GivenAnEndpointForFetchingInfoAboutAStop()
    {
      _scenarioContext.Pending();
    }
  }
}
