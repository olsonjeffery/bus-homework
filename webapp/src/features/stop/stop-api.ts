import { StopEndpointResult } from "./data";

const baseUrl = "http://bus-homework.example/api";

export function fetchUpcomingArrivalsStops1and2(): Promise<StopEndpointResult[]>
{
  return Promise.all([1, 2].map((e, index, array) => {
    return fetch(`${baseUrl}/stop/${e}`)
      .then((resp) => { return resp.json(); })
  }));
}