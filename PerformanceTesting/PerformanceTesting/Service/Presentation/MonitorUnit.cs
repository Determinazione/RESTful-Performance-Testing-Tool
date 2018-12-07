using System;
using System.Diagnostics;

namespace PerformanceTesting.Service.Presentation
{
    /// <summary>
    /// Record the status of a request.
    /// </summary>
    public struct ResponseResult
    {
        public string Tag { get; set; }
        public DateTime SubmittedTime { get; set; }
        public DateTime ReceivedTime { get; set; }
        public TimeSpan SpanningTime { get; set; }
        public bool IsRunning { get; set; }
        public bool IsSuccessful { get; set; }
        public string Result { get; set; }
    }

    class MonitorUnit
    {
        private Stopwatch _watch;
        private readonly IMonitor _monitor;
        private ResponseResult _responseResult;

        public MonitorUnit(IMonitor monitor)
        {
            _monitor = monitor;
        }

        /// <summary>
        /// Initialize recorded data.
        /// </summary>
        public void SetStartingPoint()
        {
            _responseResult = new ResponseResult();
            _responseResult.IsRunning = false;
            _monitor.SaveEmptyResult(_responseResult);
        }

        /// <summary>
        /// Starting monitor the request.
        /// </summary>
        public void StartMonitorRequest()
        {
            _watch.Reset();
            _watch = Stopwatch.StartNew();
            _responseResult.IsRunning = true;
            _responseResult.SubmittedTime = DateTime.Now;
        }

        /// <summary>
        /// Stopping monitor the request.
        /// </summary>
        public void StopMonitorRequest(bool isSuccessful, string result)
        {
            _watch.Stop();
            _responseResult.ReceivedTime = DateTime.Now;

            TimeSpan ts = _watch.Elapsed;
            _responseResult.SpanningTime = ts;
            _responseResult.IsSuccessful = isSuccessful;
            _responseResult.Result = result;
        }

        /// <summary>
        /// Save the result of the request.
        /// </summary>
        public void SaveMonitorResult()
        {
            // Save a single request result into Monitor.
            _monitor.SaveReceivedResult(_responseResult);
            // why?
            if(_monitor.UnitDataAmountIsEqualTo(Program.TasksCount))
            {
                _monitor.OutputReport();
            }
        }
    }
}
