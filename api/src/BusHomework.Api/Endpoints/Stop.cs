using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusHomework.Api.Data;

namespace BusHomework.Api.Endpoints
{
    public class Stop
    {
        public static async Task<IEnumerable<UpcomingArrival>> DoRoute(string stopId)
        {
          Console.WriteLine($"Called /stop endpoint with stopId {stopId}");
          return new List<UpcomingArrival> {
          };
        }
    }
}