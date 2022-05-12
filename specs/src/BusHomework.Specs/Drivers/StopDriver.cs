using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BusHomework.Specs.Drivers
{
  public class StopDriver
  {
    private readonly HttpClientDriver _http;
    private readonly UrlDriver _url;

    public StopDriver(HttpClientDriver httpClient, UrlDriver url)
    {
      _http = httpClient;
      _url = url;
    }

    public async Task<IEnumerable<UpcomingArrival>> GetUpcomingArrivalsFor(int stopId)
    {
      var stopUrl = _url.GetApiEndpointUrl($"/stop/{stopId}");
      var resp = await _http.Client.GetAsync(stopUrl);
      if(resp.IsSuccessStatusCode == false)
      {
        throw new Exception($"GetUpcomingArrivalsFor() call failed with resp: {resp.StatusCode} with StopId {stopId}");
      }
      var respContent = await resp.Content.ReadAsStringAsync();
      var retList = JsonConvert.DeserializeObject<IEnumerable<UpcomingArrival>>(respContent);

      return retList;
    }

    public async Task UnsetTime()
    {
      var unsetTimeUrl = _url.GetApiEndpointUrl($"/unsettime");
      var resp = await _http.Client.GetAsync(unsetTimeUrl);
      if(resp.IsSuccessStatusCode == false)
      {
        throw new Exception($"UnsetTime() call failed with resp: {resp.StatusCode}");
      }
    }

    public async Task SetTime(string callTime)
    {
      var callTimeTransportString = GetTimestampStringFromCallTime(callTime);
      var setTimeUrl = _url.GetApiEndpointUrl($"/settime/{callTimeTransportString}");
      var resp = await _http.Client.PostAsync(setTimeUrl, null);
      if(resp.IsSuccessStatusCode == false)
      {
        throw new Exception($"SetTime() call failed with resp: {resp.StatusCode} with callTime {callTime}");
      }
    }

    private string GetTimestampStringFromCallTime(string callTime)
    {
      var today = DateTime.Today.ToUniversalTime();
      var callTimeSplit = callTime.Split(":", 3, StringSplitOptions.RemoveEmptyEntries);
      
      var (hour, minute, second) = (Int32.Parse(callTimeSplit[0]), Int32.Parse(callTimeSplit[1]), Int32.Parse(callTimeSplit[2]));
      var callTimeTimespan = new TimeSpan(hour, minute, second); // assumes 24 hour time
      var callTimeTransportString = today.Add(callTimeTimespan).ToString("yyyy-M-dTHH:mm:ss");
      return callTimeTransportString;
    }
  }
}

public class UpcomingArrival
{
  public int RouteId {get;set;}
}
