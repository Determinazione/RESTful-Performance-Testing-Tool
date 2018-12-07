// Introduction: 
// Maintainer:

using System.Net;

using PerformanceTesting.Service.Presentation;
using PerformanceTesting.Service.ClientSimulator;
using PerformanceTesting.Model;

namespace PerformanceTesting.Tasks
{
    class SendingRequestTask
    {
        private readonly IWebService _service;
        private readonly IMonitor _monitor;
        private readonly MonitorUnit _monitorUnit;

        public SendingRequestTask(IWebService service, IMonitor monitor)
        {
            _service = service;
            _monitor = monitor;
            _monitorUnit = new MonitorUnit(_monitor);
        }

        public void RunTask(int index)
        {
            CgAnalysisServiceReceivedDataStructure result = new CgAnalysisServiceReceivedDataStructure()
            {
                ElasticsearchIndexName = "player_user_activate",
                DeviceUID = "7554984165",
                IsSimulator = true
            };
            _monitorUnit.SetStartingPoint();
            _monitorUnit.StartMonitorRequest();
            _service.CreateRequestContent(OnActivateResult);
        }

        private void OnActivateResult(HttpStatusCode httpStatusCode, string response)
        {
            if(httpStatusCode == HttpStatusCode.OK)
            {
                _monitorUnit.StopMonitorRequest(true, "OK");
                _monitorUnit.SaveMonitorResult();
            }
            else
            {
                _monitorUnit.StopMonitorRequest(false, $"some http issues: {httpStatusCode}");
            }
        }
    }
}
