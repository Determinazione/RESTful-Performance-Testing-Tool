using System;
using System.Net;
using System.Threading.Tasks;

namespace PerformanceTesting.Service.ClientSimulator
{
    interface IWebService
    {
        Task CreateRequestContent(Action<HttpStatusCode, string> callback);
    }
}
