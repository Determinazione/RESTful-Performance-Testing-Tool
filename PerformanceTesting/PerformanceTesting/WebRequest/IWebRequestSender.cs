using System;
using System.Net;
using System.Threading.Tasks;

namespace PerformanceTesting.WebRequest
{
    interface IWebRequestSender
    {
        Task PostAsync(string uri, string jsonData);
        Task PostAsync(string uri, string jsonData, Action<HttpStatusCode, string> callback);
    }
}
