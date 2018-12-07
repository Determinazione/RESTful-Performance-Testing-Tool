using System;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PerformanceTesting.WebRequest
{
    class WebRequestSender : IWebRequestSender
    {
        private HttpClient _httpClient;
        public  WebRequestSender(HttpClient client)
        {
            _httpClient = client;
            _httpClient.Timeout = Timeout.InfiniteTimeSpan;
        }

        /// <summary>
        /// Post method without callback function.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="jsonData"></param>
        /// <returns></returns>
        public async Task PostAsync(string uri, string jsonData)
        {
            await PostAsync(uri, jsonData, (status, responseText) => { });
        }

        public async Task PostAsync(string uri, string jsonData, Action<HttpStatusCode, string> callback)
        {
            var createdContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(uri, createdContent);
            ExamineResponse(response, callback);
        }

        /// <summary>
        /// If response isn't successful, then throw an exception.
        /// </summary>
        /// <param name="response"></param>
        /// <param name="callback"></param>
        private async void ExamineResponse(HttpResponseMessage response, Action<HttpStatusCode, string> callback)
        {
            try
            {
                string responseText = await response.Content.ReadAsStringAsync();
                if(response.StatusCode == HttpStatusCode.OK)
                {
                    Console.WriteLine($"{response.StatusCode}:{responseText}");
                    callback(response.StatusCode, responseText);
                }
                else
                {
                    response.EnsureSuccessStatusCode();
                }
            }
            catch(HttpRequestException exception)
            {
                Console.WriteLine($"{response.StatusCode}:{exception.Message}");
            }
        }
    }
}
