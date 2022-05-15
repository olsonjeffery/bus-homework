Feature: Url fetching API code
Just some small validations around the UrlDriver behavior

Scenario: well-formed leading-slash URL
  Getting a complete URL with a well-formed subpath WITH a leading slash

  Given getting an api url with the subpath "/foo"
  Then no error should have occurred

Scenario: non-leading slash URL malformation
  INVALID: Attempting to get a complete URL with a well-formed subpath WITHOUT a leading slash

  Given getting an api url with the subpath "foo"
  Then an error should have occurred