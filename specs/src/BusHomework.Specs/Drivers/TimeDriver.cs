using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BusHomework.Specs.Drivers
{
  public class TimeDriver
  {
    private readonly HttpClientDriver _http;
    private readonly UrlDriver _url;

    public TimeDriver()
    {
    }

    public string GetTimestampStringFromCallTime(string callTime)
    {
      var today = DateTime.Today.ToUniversalTime();
      var callTimeSplit = callTime.Split(":", 3, StringSplitOptions.RemoveEmptyEntries);
      
      var (hour, minute, second) = (Int32.Parse(callTimeSplit[0]), Int32.Parse(callTimeSplit[1]), Int32.Parse(callTimeSplit[2]));
      var callTimeTimespan = new TimeSpan(hour, minute, second); // assumes 24 hour time
      var callTimeTransportString = today.Add(callTimeTimespan).ToString(Constants.SendableTimestampFormatString);
      return callTimeTransportString;
    }
  }
}