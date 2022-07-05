using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Http
{
    public class WebApiClient : IWebApiClient
    {
        private readonly HttpClient _client;

        public WebApiClient(HttpClient client)
        {
            this._client = client;
        }

        public HttpResponseMessage Delete<T>(Uri addres, T transferObject)
        {
            this._client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, addres);
            request.Content = new StringContent(JsonConvert.SerializeObject(transferObject), Encoding.UTF8, "application/json");
            var response = this._client.SendAsync(request).Result;
            return response;
        }

        public HttpResponseMessage Get(Uri address, Dictionary<string, string> parameters)
        {
            var queryParameter = new List<string>();

            foreach(var parameter in parameters)
            {
                queryParameter.Add($"{parameter.Key} = {parameter.Value}");
            }

            string query = $"?{string.Join("&", queryParameter)}";

            var addressWithQuery = new Uri(address, query);

            return this._client.GetAsync(addressWithQuery).Result;
        }

        public HttpResponseMessage Patch<T>(Uri addres, T transferObject)
        {
            this._client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("PATCH"), addres);
            request.Content = new StringContent(JsonConvert.SerializeObject(transferObject), Encoding.UTF8, "application/json");
            var response = this._client.SendAsync(request).Result;
            return response;
        }

        public HttpResponseMessage Post<T>(Uri addres, T transferObject)
        {
            this._client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, addres);
            request.Content = new StringContent(JsonConvert.SerializeObject(transferObject), Encoding.UTF8, "application/json");
            var response = this._client.SendAsync(request).Result;
            return response;
        }

        public HttpResponseMessage Put<T>(Uri addres, T transferObject)
        {
            this._client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, addres);
            request.Content = new StringContent(JsonConvert.SerializeObject(transferObject), Encoding.UTF8, "application/json");
            var response = this._client.SendAsync(request).Result;
            return response;
        }
    }
}
