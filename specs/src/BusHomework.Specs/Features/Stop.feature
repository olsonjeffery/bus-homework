Feature: Stop
As a transit service provider, we would like to privder an API to our org's systems, so that riders can access information for their benefit

- There are 10 bus stops (Stops 1 - 10)
- Each stop is serviced by three routes: Routes 1, 2, and 3.
- Each stop is serviced every 15 minutes per route, 24 hours per day, and each route starts running 2 minutes after the previous one.
- Each stop is 2 minutes away from the previous one.

Scenario: Describing the Stop endpoint
  Given an endpoint for fetching info about a Stop
  When calling for Stop 1
  Then the Stop endpoint should return exactly 6 upcoming arrival results

Scenario: Calling the Stop endpoint for a specific Call Time
  Given an endpoint for fetching info about a Stop
  When calling at "00:01:00" for Stop 1
  Then the Stop endpoint should return exactly 6 upcoming arrival results
  And the Call Time should be "00:01:00"

@newtest
Scenario: Getting routes visiting a stop
  Given an endpoint for fetching info about a Stop
  When calling at "<callTime>" for Stop <stopId>
  Then the <nextRouteId1> should arrive at <nextTime1> and at <secondTime1>
  Then the <nextRouteId2> should arrive at <nextTime2> and at <secondTime2>
  Then the <nextRouteId3> should arrive at <nextTime3> and at <secondTime3>

  Examples:
    | stopId | callTime | nextRouteId1 | nextTime1 | secondTime1 | nextRouteId2 | nextTime2 | secondTime2 | nextRouteId3 | nextTime3 | secondTime3 |
    | 1      | 23:59:59 | 1            | 00:00:00  | 00:15:00    | 2            | 00:02:00  | 00:17:00    | 3            | 00:04:00  | 00:19:00    |
    | 1      | 00:00:00 | 1            | 00:15:00  | 00:30:00    | 2            | 00:02:00  | 00:17:00    | 3            | 00:04:00  | 00:19:00    |
    | 2      | 00:00:00 | 1            | 00:02:00  | 00:17:00    | 2            | 00:04:00  | 00:19:00    | 3            | 00:06:00  | 00:21:00    |

Scenario: Invalid stopId inputs to the Stop endpoint
  Anything outside of 1-10 is a no-go

  Given an endpoint for fetching info about a Stop
  When calling at "00:00:00" for Stop <stopId>
  Then an error should have occurred
  Then the http call to the api should fail

  Examples:
    | stopId                       | comment                    |
    | -1                           | INVALID: not in 1-10 range |
    | 0                            | INVALID: not in 1-10 range |
    | 11                           | INVALID: ...               |
    | 9999999999999999999999999999 | int32 overflow             |

Scenario: Invalid callTime inputs to the Stop endpoint
  Given an endpoint for fetching info about a Stop
  When calling at "<callTime>" for Stop 1
  Then an error should have occurred
  Then the http call to the api should fail

  Examples:
    | callTime                     | comment                                                 |
    | -1:00:00                     | INVALID: not within 00:00:00 to 23:59:59 range          |
    | 99:99:99                     | INVALID: ...                                            |
    | 11                           | INVALID: not in [0-9][0-9]:[0-9][0-9]:[0-9][0-9] format |
    | 9999999999999999999999999999 | int32 overflow                                          |