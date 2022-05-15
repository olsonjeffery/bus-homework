using System;

namespace BusHomework.Specs
{
  public class Constants
  {
    public const string StopIdKey = "StopIdKey";
    public const string CallTimeKey = "CallTimeKey";
    public const string UpcomingArrivalsResultKey = "UpcomingArrivalsResultKey";
    public const string AppSettingsFilename = "appSettings.Specs.json";
    public const string ApiEndpointUrlAppSettingsPath = "BusHomework:URLs:api";
    public const string SiteUrlAppSettingsPath = "BusHomework:URLs:webapp";
    public const string SendableTimestampFormatString = "yyyy-M-dTHH:mm:ss";

    public const string LastOperationExceptionKey = "LastOperationExceptionKey";

    public const string LastHttpCallFailed = "LastHttpCallFailed";

    public static TimeSpan StandardWaitTimeout = new TimeSpan(0, 0, 15);

  }
}