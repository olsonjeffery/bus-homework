using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusHomework.Api.Data;

namespace BusHomework.Api.Services
{
    public interface IStopRepository
    {
      Task<IEnumerable<UpcomingArrival>> GetUpcomingArrivalsFor(int stopId, DateTime callTime);
    }
  public class StopRepository : IStopRepository
  {
    public async Task<IEnumerable<UpcomingArrival>> GetUpcomingArrivalsFor(int stopId, DateTime callTime)
    {
      return new[] {
        new UpcomingArrival { ArrivalTime = DateTime.Now.ToString(Constants.SendableTimestampFormatString), RouteId = 1},
        new UpcomingArrival { ArrivalTime = DateTime.Now.ToString(Constants.SendableTimestampFormatString), RouteId = 2}
      };
    }
  }
}