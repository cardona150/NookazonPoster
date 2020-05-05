using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace NookazonPoster
{
  class Program
  {
    static int throttleMs = 1000;

    static async Task Main(string[] args)
    {
      var httpClient = new HttpClient();
      httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "YourTokenHere");
      await DeleteListingsAsync(httpClient);
      await PostListingsAsync(httpClient);
    }

    static void LogRepsonse(HttpResponseMessage httpResponseMessage)
    {
      Console.WriteLine($"{httpResponseMessage.RequestMessage.RequestUri} {(httpResponseMessage.IsSuccessStatusCode ? "Success" : "Failure")}");
    }

    static async Task DeleteListingsAsync(HttpClient httpClient)
    {

      var httpResponseMessage = await httpClient.GetAsync("https://nookazon.com/api/listings?seller=1297959873");
      LogRepsonse(httpResponseMessage);
      var listingsResponse = JsonSerializer.Deserialize<ListingsResponse>(await httpResponseMessage.Content.ReadAsStringAsync());

      foreach (var listing in listingsResponse.listings)
      {
        var stringContent = new StringContent($"{{\"listing\":\"{listing.id}\",\"selling\":true,\"page\":0,\"remove\":true}}", Encoding.UTF8, "application/json");
        httpResponseMessage = await httpClient.PostAsync("https://nookazon.com/api/sell", stringContent);
        LogRepsonse(httpResponseMessage);
        Thread.Sleep(throttleMs);
      }
    }

    static async Task PostListingsAsync(HttpClient httpClient)
    {
      var posts = new List<Post>()
      {
        new Post()
        {
          asks = new string[] {
            //"\"bells\":3960000",
            "\"nmt\":40",
            "\"items\":[{\"quantity\":40,\"value\":\"2041463462\"}]",
            "\"items\":[{\"quantity\":8,\"value\":\"152074168\"}]",
            // Lucky Cats
            "\"items\":[{\"quantity\":1,\"value\":\"1316751206\",\"variant\":{\"id\":1428518398}}]",
            "\"items\":[{\"quantity\":1,\"value\":\"1316751206\",\"variant\":{\"id\":1730239661}}]",
            // Pagodas
            "\"items\":[{\"quantity\":1,\"value\":\"513802910\",\"variant\":{\"id\":1516397956}}]",
            "\"items\":[{\"quantity\":1,\"value\":\"513802910\",\"variant\":{\"id\":626353939}}]",
            "\"items\":[{\"quantity\":1,\"value\":\"513802910\",\"variant\":{\"id\":886592442}}]",
            },
          // Moon chair
          id = "1164736023",
          //variants = new int[] { 821499116, 799427259, 1324580107, 1594518544, 2107659061, 479073513, 1830641310 }
          variants = new int[] { 799427259 }
        },
        //new Post()
        //{
        //  asks = new string[] {
        //    "\"nmt\":40",
        //    "\"items\":[{\"quantity\":40,\"value\":\"2041463462\"}]",
        //    "\"items\":[{\"quantity\":8,\"value\":\"152074168\"}]",
        //    // Lucky Cats
        //    "\"items\":[{\"quantity\":1,\"value\":\"1316751206\",\"variant\":{\"id\":1428518398}}]",
        //    "\"items\":[{\"quantity\":1,\"value\":\"1316751206\",\"variant\":{\"id\":1730239661}}]",
        //    // Pagodas
        //    //"\"items\":[{\"quantity\":1,\"value\":\"513802910\",\"variant\":{\"id\":1516397956}}]",
        //    //"\"items\":[{\"quantity\":1,\"value\":\"513802910\",\"variant\":{\"id\":626353939}}]",
        //    //"\"items\":[{\"quantity\":1,\"value\":\"513802910\",\"variant\":{\"id\":886592442}}]",
        //    },
        //  // Katana
        //  id = "1526871784",
        //  variants = new int[] { 341796602, 453985379, 1794978450, 641927087, 1197763550 }
        //},
        //new Post()
        //{
        //  asks = new string[] {
        //    "\"nmt\":80",
        //    "\"items\":[{\"quantity\":80,\"value\":\"2041463462\"}]",
        //    "\"items\":[{\"quantity\":16,\"value\":\"152074168\"}]",
        //    // Pagodas
        //    "\"items\":[{\"quantity\":1,\"value\":\"513802910\",\"variant\":{\"id\":1516397956}}]",
        //    "\"items\":[{\"quantity\":1,\"value\":\"513802910\",\"variant\":{\"id\":626353939}}]",
        //    "\"items\":[{\"quantity\":1,\"value\":\"513802910\",\"variant\":{\"id\":886592442}}]",
        //    // Robot Hero
        //    "\"items\":[{\"quantity\":1,\"value\":\"917724281\",\"variant\":{\"id\":1133710032}}]",
        //    "\"items\":[{\"quantity\":1,\"value\":\"917724281\",\"variant\":{\"id\":1912940794}}]",
        //    "\"items\":[{\"quantity\":1,\"value\":\"917724281\",\"variant\":{\"id\":1659469289}}]",
        //    "\"items\":[{\"quantity\":1,\"value\":\"917724281\",\"variant\":{\"id\":537745724}}]",
        //    "\"items\":[{\"quantity\":1,\"value\":\"917724281\",\"variant\":{\"id\":1358781870}}]",
        //    "\"items\":[{\"quantity\":1,\"value\":\"917724281\",\"variant\":{\"id\":832531592}}]",
        //    "\"items\":[{\"quantity\":1,\"value\":\"917724281\",\"variant\":{\"id\":1055731396}}]",
        //    "\"items\":[{\"quantity\":1,\"value\":\"917724281\",\"variant\":{\"id\":238106733}}]",
        //    },
        //  // Lucky Cat
        //  id = "1316751206",
        //  variants = new int[] { 1428518398, 1730239661 }
        //},
        //new Post()
        //{
        //  asks = new string[] {
        //    "\"nmt\":90",
        //    "\"items\":[{\"quantity\":90,\"value\":\"2041463462\"}]",
        //    "\"items\":[{\"quantity\":18,\"value\":\"152074168\"}]",
        //    // Robot Hero
        //    "\"items\":[{\"quantity\":1,\"value\":\"917724281\",\"variant\":{\"id\":1133710032}}]",
        //    "\"items\":[{\"quantity\":1,\"value\":\"917724281\",\"variant\":{\"id\":1912940794}}]",
        //    "\"items\":[{\"quantity\":1,\"value\":\"917724281\",\"variant\":{\"id\":1659469289}}]",
        //    "\"items\":[{\"quantity\":1,\"value\":\"917724281\",\"variant\":{\"id\":537745724}}]",
        //    "\"items\":[{\"quantity\":1,\"value\":\"917724281\",\"variant\":{\"id\":1358781870}}]",
        //    "\"items\":[{\"quantity\":1,\"value\":\"917724281\",\"variant\":{\"id\":832531592}}]",
        //    "\"items\":[{\"quantity\":1,\"value\":\"917724281\",\"variant\":{\"id\":1055731396}}]",
        //    "\"items\":[{\"quantity\":1,\"value\":\"917724281\",\"variant\":{\"id\":238106733}}]",
        //    },
        //  // Pagoda
        //  id = "513802910",
        //  variants = new int[] { 1516397956, 626353939, 886592442 }
        //},
        //new Post()
        //{
        //  asks = new string[] {
        //    "\"nmt\":100",
        //    "\"items\":[{\"quantity\":100,\"value\":\"2041463462\"}]",
        //    "\"items\":[{\"quantity\":20,\"value\":\"152074168\"}]",
        //    },
        //  // Robot Hero
        //  id = "917724281",
        //  variants = new int[] { 1133710032, 1912940794, 1659469289, 537745724, 1358781870, 832531592, 1055731396, 238106733 }
        //},
      };

      foreach (var post in posts)
      {
        foreach (var variant in post.variants)
        {
          foreach (var ask in post.asks)
          {
            var stringContent = new StringContent($"{{\"amount\":1,\"item\":\"{post.id}\",\"selling\":true,{ask},\"variant\":{variant}}}", Encoding.UTF8, "application/json");
            var httpResponseMessage = await httpClient.PostAsync("https://nookazon.com/api/listings/create", stringContent);
            LogRepsonse(httpResponseMessage);
            Thread.Sleep(throttleMs);
          }
        }
      }
    }
  }

  class ListingsResponse
  {
    public Listing[] listings { get; set; }
  }

  class Listing
  {
    public string id { get; set; }
  }

  class Post
  {
    public string id { get; set; }
    public int[] variants { get; set; }
    public string[] asks { get; set; }
  }
}
