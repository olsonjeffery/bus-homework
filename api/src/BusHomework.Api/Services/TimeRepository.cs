using System;

namespace BusHomework.Api.Services
{
  public interface ITimeRepository
  {
    DateTime GetNowTime();
    DateTime GetTimeFrom(string timestamp);
  }

  public class TimeRepository : ITimeRepository
  {
    public DateTime GetNowTime()
    {
      return DateTime.Now.ToUniversalTime();
    }

    public DateTime GetTimeFrom(string timestamp)
    {
      return DateTime.SpecifyKind(DateTime.Parse(timestamp), DateTimeKind.Utc);
    }
  }
}