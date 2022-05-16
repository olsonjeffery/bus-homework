Feature: Client Webapp Default Stop
As a transit service provider, we would like the webapp to have a default Stop display, so that riders can have a default view per the requested feature-set

Scenario: Default Tab is selected when page loads
  Given a visitor navigates to the landing page
  Then the selected AppBar entry should be "Stops 1 & 2"

Scenario: Stop info is populated in page and shows Stops 1 & 2
  Given a visitor navigates to the landing page
  When the Stops section is loaded and available
  Then Stop details for Stop 1 should be showing
  Then Stop details for Stop 2 should be showing