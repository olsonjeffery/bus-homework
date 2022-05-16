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

    [Then("the outcome should match (.*)")]
    public void ThenTheOutcomeShouldMatch(bool isValid)
    {
      Assert.AreEqual(_scenarioContext.ContainsKey(Constants.LastOperationExceptionKey), !isValid);
      if(!isValid)
      {
        Assert.IsTrue((bool)_scenarioContext[Constants.LastOperationExceptionKey]);
      }
    }

    [Then("the http call to the api should fail")]
    public void ThenTheHttpCallToTheApiShouldFail()
    {
      Assert.IsTrue((bool)_scenarioContext[Constants.LastHttpCallFailed]);
    }

    [Then("an error should have occurred")]
    public void ThenAnErrorShouldHaveOccurred()
    {
      Assert.IsTrue((bool)_scenarioContext[Constants.LastOperationExceptionKey]);
    }

    [Then("no error should have occurred")]
    public void ThenNoErrorShouldHaveOccurred()
    {
      Assert.IsFalse(_scenarioContext.ContainsKey(Constants.LastOperationExceptionKey));
    }
  }
}
