Feature: Client Webapp
As a transit service provider, we would like to provide a mobile-friendly website for riders, so that they can access information about upcoming arrivals at stops

Scenario: Happy path navigation to landing page
  Given a visitor navigates to the landing page
  Then the page title should be "Bus Homework Rider Webapp"

Scenario: Contents of the top app bar
  Given a visitor navigates to the landing page
  Then the AppBar should contain a "Default" section
  And the AppBar should contain a "Custom" section
  And the AppBar should contain an "Advanced" section