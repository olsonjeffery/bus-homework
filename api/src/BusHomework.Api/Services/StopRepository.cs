using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
    private static IEnumerable<(int, IEnumerable<ArrivalAtStop>)>? _arrivalsByRoute = null;
    private static SemaphoreSlim _lock = new SemaphoreSlim(1, 1);

    public async Task<IEnumerable<UpcomingArrival>> GetUpcomingArrivalsFor(int stopId, DateTime callTime)
    {
      await PopulateArrivalsByRoute();

      return await GetUpcomingArrivalsAtStopForCallTime(stopId, callTime);
    }

    private async Task<IEnumerable<UpcomingArrival>> GetUpcomingArrivalsAtStopForCallTime(int stopId, DateTime callTime)
    {
      var upcomingArrivals = new List<UpcomingArrival>();

      if (_arrivalsByRoute == null)
      {
        throw new Exception("Unexpected null value for Arrivals By Route; shouldn't happen");
      }

      foreach (var currTuple in _arrivalsByRoute)
      {
        var (routeId, routeArrivals) = currTuple;
        var allArrivalsAtStopByTime = routeArrivals.Where(x=>x.StopId == stopId).OrderBy(x=>x.ArrivalTime).ToList();
        var arrivalCandidates = allArrivalsAtStopByTime.Where(x=>x.ArrivalTime > callTime).ToList();
        if(arrivalCandidates.Count() == 0) // perfect storm!
        {
          upcomingArrivals.AddRange(allArrivalsAtStopByTime.Take(2).Select(x=>new ArrivalAtStop {ArrivalTime = x.ArrivalTime.AddDays(1), StopId = x.StopId}).Select(x=>ToUpcomingArrival(routeId, x)));
        }
        else if (arrivalCandidates.Count() == 1)
        {
          arrivalCandidates.Add(allArrivalsAtStopByTime.Select(x=>new ArrivalAtStop {ArrivalTime = x.ArrivalTime.AddDays(1), StopId = x.StopId}).First());
          upcomingArrivals.AddRange(arrivalCandidates.Select(x=>ToUpcomingArrival(routeId, x)));
        }
        else
        {
          upcomingArrivals.AddRange(arrivalCandidates.Take(2).Select(x=>ToUpcomingArrival(routeId, x)));
        }
      }

      return upcomingArrivals;
    }

    private UpcomingArrival ToUpcomingArrival(int routeId, ArrivalAtStop x)
    {
      return new UpcomingArrival {RouteId = routeId, ArrivalTime = x.ArrivalTime.ToString(Constants.SendableTimestampFormatString)};
    }

    private async Task PopulateArrivalsByRoute()
    {
      await _lock.WaitAsync();
      if (_arrivalsByRoute == null)
      {
        Console.WriteLine("Doing arrival population");
        var route1Arrivals = ProduceArrivalsWithABaseOffsetFromMidnightStopCountTravelTimeAndVisitFrequency(0, 10, 2, 15);
        var route2Arrivals = ProduceArrivalsWithABaseOffsetFromMidnightStopCountTravelTimeAndVisitFrequency(2, 10, 2, 15);
        var route3Arrivals = ProduceArrivalsWithABaseOffsetFromMidnightStopCountTravelTimeAndVisitFrequency(4, 10, 2, 15);
        _arrivalsByRoute = new List<(int, IEnumerable<ArrivalAtStop>)> { (1, route1Arrivals), (2, route2Arrivals), (3, route3Arrivals) };
        Console.WriteLine("Completed arrival population");
      }
      _lock.Release();
    }

    /// <summary>
    /// all times are in minutes
    /// </summary>
    /// <param name="offsetFromMidnight"></param>
    /// <param name="v2"></param>
    /// <param name="v3"></param>
    /// <returns></returns>
    private IEnumerable<ArrivalAtStop> ProduceArrivalsWithABaseOffsetFromMidnightStopCountTravelTimeAndVisitFrequency(
      int offsetFromMidnight, int stopCount, int travelTimeBetweenStops, int stopVisitFrequency)
    {
      var upcomingArrivalsForRoute = new List<ArrivalAtStop>();

      var numberOfVisits = 1440 / stopVisitFrequency;

      var baseTime = new TimeSpan(0, offsetFromMidnight, 0);
      var startTimeAtFirstStop = DateTime.Today.Add(baseTime);
      int currStop = 0;
      do
      {

        var visitCounter = 0;
        while (visitCounter < numberOfVisits)
        {
          var additionalMinutesForVisit = visitCounter * stopVisitFrequency;

          var additionalMinutesToTravelToStop = currStop * travelTimeBetweenStops;
          var timeOffset = new TimeSpan(0, additionalMinutesForVisit + additionalMinutesToTravelToStop, 0);

          var finalTime = startTimeAtFirstStop + timeOffset;

          upcomingArrivalsForRoute.Add(new ArrivalAtStop { ArrivalTime = finalTime, StopId = currStop+1 });
          visitCounter += 1;
        }

        currStop += 1;

      } while (currStop < stopCount);

      return upcomingArrivalsForRoute;
    }
  }
}