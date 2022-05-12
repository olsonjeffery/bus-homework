using System.IO;
using Microsoft.Extensions.Configuration;

namespace BusHomework.Specs.Drivers
{
  public class AppSettingsDriver
  {
    public IConfiguration Configuration { get { return _configuration; } }
    public static IConfiguration _configuration = new ConfigurationBuilder()
      .SetBasePath(Directory.GetCurrentDirectory())
      .AddJsonFile(Constants.AppSettingsFilename, false)
      .Build();
  }
}