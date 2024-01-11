using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TaxServiceAdaptor
{

    public interface IHttpService
    {
        Task<TOut> PostAsync<TIn, TOut>(TIn request, string url);
        Task<T> SendAsync<T>(HttpRequestMessage requestMessage);
    }
    public class HttpService : IHttpService
    {
        public HttpService() { }

        public async Task<TOut> PostAsync<TIn, TOut>(TIn _request, string url)
        {
            var requestUri = new Uri(url);
            var body = new { message = new { body = new { data = _request } } };
            var payload = JsonConvert.SerializeObject(body, new JsonSerializerSettings { Formatting = Formatting.Indented });

            var httpContent = new StringContent(payload, Encoding.UTF8, "application/json");
            var requestMessage = new HttpRequestMessage() {
                Method = HttpMethod.Post,
                RequestUri = requestUri,
                Content = httpContent
            };
            return await SendAsync<TOut>(requestMessage);
        }
        public async Task<T> SendAsync<T>(HttpRequestMessage requestMessage)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var result = await client.SendAsync(requestMessage);
                    result.EnsureSuccessStatusCode();

                    var response = await result.Content.ReadAsStringAsync();
                    var innerDef = new { message = new { body = new { data = new Object { } } } };
                    var obj = JsonConvert.DeserializeAnonymousType(response, innerDef);
                    var serialized = JsonConvert.SerializeObject(obj.message.body.data);

                    return JsonConvert.DeserializeObject<T>(serialized);
                }
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }
    }
}