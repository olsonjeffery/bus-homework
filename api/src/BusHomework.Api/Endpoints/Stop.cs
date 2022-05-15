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
          return await GetArrivalsWith(stopId, nowTime, stops);
        }

        public static async Task<StopEndpointResult> StopWithStopIdAndCallTime(int stopId, string timestamp, IStopRepository stops, ITimeRepository time)
        {
          var nowTime = time.GetTimeFrom(timestamp);
          return await GetArrivalsWith(stopId, nowTime, stops);
        }

        private static async Task<StopEndpointResult> GetArrivalsWith(Int32 stopId, DateTime callTime, IStopRepository stops)
        {

          if(!ValidateStopId(stopId))
          {
            throw new Exception("Invalid stopId input");
          }
          var result = await stops.GetUpcomingArrivalsFor(stopId, callTime);
          var retVal = new StopEndpointResult
          {
            CallTimestamp = callTime.ToString(Constants.SendableTimestampFormatString),
            UpcomingArrivals = result
          };

          Console.WriteLine($"Input: stopId {stopId} converted time: {callTime.ToString(Constants.SendableTimestampFormatString)} Output: {System.Text.Json.JsonSerializer.Serialize(retVal)}");

          return retVal;
        }

        private static bool ValidateStopId(Int32 stopId)
        {
          if(stopId < 1 || stopId > 10)
          {
            return false;
          }
          return true;
        }
    }
}