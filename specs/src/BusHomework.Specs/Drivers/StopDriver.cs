using System;
using System.Collections.Generic;

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

    public IEnumerable<UpcomingArrival> GetUpcomingArrivalsAt(object stopId, string callTime)
    {
      var url = _url.GetApiEndpointUrl("/stop");
      var retList = new List<UpcomingArrival>();

      return retList;
    }
  }
}

public class UpcomingArrival
{
  public int RouteId {get;set;}
}
