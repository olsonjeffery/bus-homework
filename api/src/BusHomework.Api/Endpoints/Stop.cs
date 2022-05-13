using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusHomework.Api.Data;
using BusHomework.Api.Services;

namespace BusHomework.Api.Endpoints
{
    public class Stop
    {
        public static async Task<StopEndpointResult> StopWithStopId(int stopId, IStopRepository stops, ITimeRepository time)
        {
          var nowTime = time.GetNowTime();
          var result = await stops.GetUpcomingArrivalsFor(stopId, nowTime);
          return new StopEndpointResult
          {
            CallTimestamp = nowTime.ToString(Constants.SendableTimestampFormatString),
            UpcomingArrivals = result
          };
        }

        public static async Task<StopEndpointResult> StopWithStopIdAndCallTime(int stopId, string timestamp, IStopRepository stops, ITimeRepository time)
        {
          var nowTime = time.GetTimeFrom(timestamp);
          var result = await stops.GetUpcomingArrivalsFor(stopId, nowTime);
          var retVal = new StopEndpointResult
          {
            CallTimestamp = nowTime.ToString(Constants.SendableTimestampFormatString),
            UpcomingArrivals = result
          };

          Console.WriteLine($"Input: stopId {stopId} timestamp '{timestamp}' converted time: {nowTime.ToString(Constants.SendableTimestampFormatString)} Output: {System.Text.Json.JsonSerializer.Serialize(retVal)}");

          return retVal;
        }
    }
}