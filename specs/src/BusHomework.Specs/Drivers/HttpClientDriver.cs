using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BusHomework.Specs.Drivers
{
  public class HttpClientDriver
  {
    private static HttpClient _client = new HttpClient();
    public HttpClient Client { get { return _client; } }
    public HttpClientDriver() { }

    public async Task<T> GetAndDeserialize<T>(string targetUrl)
    {

      var resp = await Client.GetAsync(targetUrl);
      if(resp.IsSuccessStatusCode == false)
      {
        throw new Exception($"GetAndDeserialize() call failed with resp: {resp.StatusCode} with url {targetUrl}");
      }
      var respContent = await resp.Content.ReadAsStringAsync();
      return JsonConvert.DeserializeObject<T>(respContent);
    }
  }
}