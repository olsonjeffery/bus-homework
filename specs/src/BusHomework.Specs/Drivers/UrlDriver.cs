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
      var prefix = _config.Configuration[Constants.ApiEndpointUrlAppSettingsPath];
      return GetUrlWith(prefix, apiPath);
    }

    public string GetWebappUrl(string sitePath)
    {
      var prefix = _config.Configuration[Constants.SiteUrlAppSettingsPath];
      return GetUrlWith(prefix, sitePath);
    }

    private string GetUrlWith(string prefix, string relativePath)
    {
      if(!relativePath.StartsWith("/"))
      {
        throw new Exception("GetUrlWith(): input must have leading slash to integration with config-stored path");
      }
      return $"{prefix}{relativePath}";
    }
  }
}