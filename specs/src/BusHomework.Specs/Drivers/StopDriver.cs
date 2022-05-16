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
    private readonly TimeDriver _time;

    public StopDriver(HttpClientDriver httpClient, UrlDriver url, TimeDriver time)
    {
      _http = httpClient;
      _url = url;
      _time = time;
    }

    public async Task<StopEndpointResult> GetUpcomingArrivalsFor(string stopId)
    {
      var stopUrl = _url.GetApiEndpointUrl($"/stop/{stopId}");
      return await _http.GetAndDeserialize<StopEndpointResult>(stopUrl);
    }

    public async Task<StopEndpointResult> GetUpcomingArrivalsFor(string stopId, string callTime)
    {
      var timestamp = callTime;
      var stopUrl = _url.GetApiEndpointUrl($"/stop/{stopId}/time/{callTime}");
      return await _http.GetAndDeserialize<StopEndpointResult>(stopUrl);
    }
  }

  public class StopEndpointResult
  {
    public IEnumerable<UpcomingArrival> UpcomingArrivals {get;set;} = new UpcomingArrival[0];
    public string CallTimestamp {get;set;} = "";
    public int StopId {get;set;}

  }
  public class UpcomingArrival
  {
    public int RouteId {get;set;}
    public string ArrivalTime {get;set;} = "";
  }
}
