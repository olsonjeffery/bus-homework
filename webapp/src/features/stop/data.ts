export interface UpcomingArrival
{
  routeId: number,
  arrivalTime: string
}

export interface StopEndpointResult
{
  upcomingArrivals: Array<UpcomingArrival>;
  callTimestamp: string;
  stopId: number;
}