using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;

namespace BusHomework.Specs.Drivers
{
  public class HttpClientDriver
  {
    private static HttpClient _client = new HttpClient();
    private readonly ScenarioContext _ctx;
    private readonly OutputDriver _outputHelper;

    public HttpClient Client { get { return _client; } }
    public HttpClientDriver(ScenarioContext ctx, OutputDriver outputHelper)
    {
      _ctx = ctx;
      _outputHelper = outputHelper;
    }

    public async Task<T> GetAndDeserialize<T>(string targetUrl)
    {

      var resp = await Client.GetAsync(targetUrl);
      if(resp.IsSuccessStatusCode == false)
      {
        Fail(resp, targetUrl);
      }
      var respContent = await resp.Content.ReadAsStringAsync();
      if(respContent == null) {
        Fail(resp, targetUrl);
        throw new Exception("Will never reach this; this is here to satisfy compiler wrt Nullable");
      }
      var result = JsonConvert.DeserializeObject<T>(respContent);
      if(result == null)
      {
        Fail(resp, targetUrl); // haha even though this throws it doesn't count this as a null check
        throw new Exception("Will never reach this; this is here to satisfy compiler wrt Nullable");
      }
      return result;
    }

    private void Fail(HttpResponseMessage resp, string targetUrl)
    {
        _ctx[Constants.LastHttpCallFailed] = true;
        _outputHelper.OutputThenThrow($"GetAndDeserialize() call failed with resp: {resp.StatusCode} with url {targetUrl}");
    }
  }
}