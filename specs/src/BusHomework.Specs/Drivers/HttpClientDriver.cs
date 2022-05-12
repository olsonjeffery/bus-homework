using System.Net.Http;

namespace BusHomework.Specs.Drivers
{
    public class HttpClientDriver
    {
      private static HttpClient _client = new HttpClient();
      public HttpClient Client { get { return _client; }}
      public HttpClientDriver() {}
    }
}