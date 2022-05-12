using System.Net.Http;

namespace BusHomework.Specs.Drivers
{
    public class HttpClientDriver
    {
      public static HttpClient Client = new HttpClient();
      public HttpClientDriver() {}
    }
}