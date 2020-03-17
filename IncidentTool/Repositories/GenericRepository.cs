using IncidentTool.Constants;
using IncidentTool.Interfaces.Repositories;
using Newtonsoft.Json;
using Polly;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace IncidentTool.Repositories
{
    // 'Generic' repository omdat deze voor ieder object gebruikt kan worden
    public class GenericRepository : IGenericRepository
    {
        public async Task<T> GetAsync<T>(string uri)
        {
            try
            {
                HttpClient httpClient = CreateHttpClient();
                string jsonResult = string.Empty;

                HttpResponseMessage responseMessage = await httpClient.GetAsync(uri);

                if (responseMessage.IsSuccessStatusCode)
                {
                    jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var json = JsonConvert.DeserializeObject<T>(jsonResult);
                    return json;
                }

                throw new HttpRequestException();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"{ e.GetType().Name + " : " + e.Message}");
                throw;
            }
        }

        public async Task PostAsync<T>(string uri, T data)
        {
            try
            {
                HttpClient httpClient = CreateHttpClient();

                StringContent content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string jsonResult = string.Empty;

                var responseMessage = await httpClient.PostAsync(uri, content);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"{ e.GetType().Name + " : " + e.Message}");
            }
        }

        public async Task PutAsync<T>(string uri)
        {
            try
            {
                HttpClient httpClient = CreateHttpClient();
                string jsonResult = string.Empty;

                HttpResponseMessage responseMessage = await httpClient.GetAsync(uri);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"{ e.GetType().Name + " : " + e.Message}");
                throw;
            }
        }

        private HttpClient CreateHttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient;
        }
    }
}
