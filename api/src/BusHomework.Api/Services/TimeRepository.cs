using System;

namespace BusHomework.Api.Services
{
  public interface ITimeRepository
  {
    DateTime GetNowTime();
    void SetTime(DateTime newTime);
    void UnsetTime();
  }

  public class TimeRepository : ITimeRepository
  {
    private static DateTime? _optOutTime;

    public DateTime GetNowTime()
    {
      return _optOutTime ?? DateTime.Now;
    }

    public void SetTime(DateTime newTime)
    {
      _optOutTime = newTime;
    }

    public void UnsetTime()
    {
      _optOutTime = null;
    }
  }
}