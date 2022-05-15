using System;

namespace BusHomework.Specs.Drivers
{
  public class UrlDriver
  {
    private readonly AppSettingsDriver _config;

    public UrlDriver(AppSettingsDriver config)
    {
      _config = config;
    }

    /// <summary>
    /// Provide your own leading slash!
    /// </summary>
    /// <param name="apiPath">provide a leading slash!</param>
    /// <returns></returns>
    public string GetApiEndpointUrl(string apiPath)
    {
      if(!apiPath.StartsWith("/"))
      {
        throw new Exception("apiPath input must have leading slash to integration with config-stored path");
      }
      var prefix = _config.Configuration[Constants.ApiEndpointUrlAppSettingsPath];
      return $"{prefix}{apiPath}";
    }
  }
}