using System;
using System.Text.RegularExpressions;

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
      return DateTime.Now;
    }

    public DateTime GetTimeFrom(string timestamp)
    {
      if(!ValidateTimestamp(timestamp))
      {
        throw new Exception("timestamp failed validation");
      }
      return DateTime.SpecifyKind(DateTime.Parse(timestamp), DateTimeKind.Utc);
    }

    private bool ValidateTimestamp(string timestamp)
    {
      if(!new Regex("[0-9][0-9]:[0-9][0-9]:[0-9][0-9]").IsMatch(timestamp))
      {
        // nice to have: log why it failed vaildation, give feedback to user
        return false;
      }
      return true;
    }
  }
}