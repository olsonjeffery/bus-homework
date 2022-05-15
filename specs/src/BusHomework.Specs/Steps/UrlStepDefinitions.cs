using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusHomework.Specs.Drivers;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace BusHomework.Specs.Steps
{
  [Binding]
  public class UrlStepDefinitions
  {
    private readonly ScenarioContext _ctx;
    private readonly UrlDriver _url;
    private readonly OutputDriver _output;

    public UrlStepDefinitions(ScenarioContext ctx, UrlDriver url, OutputDriver output)
    {
      _ctx = ctx;
      _url = url;
      _output = output;
    }

    [Given("getting an api url with the subpath \"(.*)\"")]
    public void GivenGettingAUrlWithTheSubpath(string subpath)
    {
      try
      {
        _url.GetApiEndpointUrl(subpath);
      }
      catch (Exception e)
      {
        _output.OutputExceptionContent(e);
        _ctx[Constants.LastOperationExceptionKey] = true;
      }
    }
  }
}