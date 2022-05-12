Feature: Stop
As a transit rider, I would like to know about which Routes are servicing which Stops

In this system we have 10 Stops (numbers 1 through 10)

Scenario: Describing the Stop endpoint
Given an endpoint for fetching info about a Stop
When calling at "callTime" for Stop 1
Then the Stop endpoint should return exactly two upcoming arrival results

Scenario: Getting routes visiting a stop
	Given a call to fetch arrivals at stop <stopId>
  When calling at "callTime"
  Then the <nextRouteId> should arrive at <nextTime> and the <secondRouteId> should arrive at <secondTime>

  Examples:
    | stopId | callTime | nextRouteId | nextTime | secondRouteId | secondTime |
    | 1      | 23:59:59 | 1           | 00:00:00 | 2             | 00:02:00   |

  Scenario: Invalid stopId inputs to the Stop endpoint
  Anything outside of 1-10 is a no-go

  Scenario: Invalid callTime inputs to the Stop endpoint

  Scenario: Stop endpoint returns arrival times in UTC timezone offset

  Scenario: Requesting info f