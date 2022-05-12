Feature: Stop

- There are 10 bus stops (Stops 1 - 10)
- Each stop is serviced by three routes: Routes 1, 2, and 3.
- Each stop is serviced every 15 minutes per route, 24 hours per day, and each route starts running 2 minutes after the previous one.
- Each stop is 2 minutes away from the previous one.


Scenario: Describing the Stop endpoint
  Given an endpoint for fetching info about a Stop
  When calling at "00:00:00" for Stop 1
  Then the Stop endpoint should return exactly two upcoming arrival results

Scenario: Getting routes visiting a stop
	Given a call to fetch arrivals at stop <stopId>
  When calling at <callTime>
  Then the <nextRouteId> should arrive at <nextTime> and the <secondRouteId> should arrive at <secondTime>

  Examples:
    | stopId | callTime | nextRouteId | nextTime | secondRouteId | secondTime |
    | 1      | 23:59:59 | 1           | 00:00:00 | 2             | 00:02:00   |

Scenario: Invalid stopId inputs to the Stop endpoint
  Anything outside of 1-10 is a no-go

Scenario: Invalid callTime inputs to the Stop endpoint

Scenario: Stop endpoint returns arrival times in UTC timezone offset
