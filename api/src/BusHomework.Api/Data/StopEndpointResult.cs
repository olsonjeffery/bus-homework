using System.Collections.Generic;

namespace BusHomework.Api.Data
{
    public class StopEndpointResult
    {
        public IEnumerable<UpcomingArrival> UpcomingArrivals {get;set;} = new UpcomingArrival[0];
        public string CallTimestamp {get;set;} = "";
    }
}