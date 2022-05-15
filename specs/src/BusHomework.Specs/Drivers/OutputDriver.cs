using System;
using TechTalk.SpecFlow.Infrastructure;

namespace BusHomework.Specs.Drivers
{
  public class OutputDriver
  {
    private readonly ISpecFlowOutputHelper _helper;

    public OutputDriver(ISpecFlowOutputHelper helper)
    {
      _helper = helper;
    }

    internal void OutputThenThrow(string message)
    {
      Output(message);
      throw new Exception(message);
    }

    internal void Output(string message)
    {
      // opportunity to inject timestamp, env info etc here
      _helper.WriteLine(message);
    }

    internal void OutputExceptionContent(Exception e)
    {
      var optionalInner = "";
      if(e.InnerException != null)
      {
        optionalInner = $"\n\nINNER {e.InnerException.GetType()} -- {e.InnerException.Message}\n\nINNER STACKTRACE: {e.InnerException.StackTrace}";
      }
      var message = $"EXCEPTION: {e.GetType()} -- {e.Message}\n\nSTACKTRACE: {e.StackTrace}\n\nHAS INNER? {e.InnerException != null}{optionalInner}";
      _helper.WriteLine(message);
    }
  }
}