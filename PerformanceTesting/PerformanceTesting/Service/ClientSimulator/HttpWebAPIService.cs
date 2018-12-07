using System.Threading.Tasks;

using PerformanceTesting.Service.ClientSimulator;
using PerformanceTesting.Service.Presentation;
using PerformanceTesting.Tasks;

namespace PerformanceTesting.Service.ClientSimulator
{
    class HttpWebAPIService
    {
        private readonly IWebService _ClientSimulator;
        private readonly IMonitor _monitor;
        private int connetionTime = 0;

        HttpWebAPIService(IWebService service, IMonitor monitor)
        {
            _ClientSimulator = service;
            _monitor = monitor;
        }

        public void StartNewTask()
        {
            var service = new SendingRequestTask(_ClientSimulator, _monitor);
            Task task = Task.Factory.StartNew(() =>
            {
                ++connetionTime;
                service.RunTask(connetionTime);
            });
        }
    }
}
