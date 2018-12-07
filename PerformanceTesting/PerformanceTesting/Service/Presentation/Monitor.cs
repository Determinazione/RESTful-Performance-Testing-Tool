using System;
using System.Collections.Generic;

namespace PerformanceTesting.Service.Presentation
{
    class Monitor : IMonitor
    {
        private static readonly List<ResponseResult> _emptyResponseData = new List<ResponseResult>();
        private static readonly List<ResponseResult> _responseData = new List<ResponseResult>();

        /// <summary>
        /// For the scenario that a MonitorUnit been initialized successfully but can't receive response.
        /// </summary>
        /// <param name="requestResult"></param>
        public void SaveEmptyResult(ResponseResult requestResult)
        {
            // Why do we need lock here?
            lock(_emptyResponseData)
            {
                _emptyResponseData.Add(requestResult);
            }
        }

        public void SaveReceivedResult(ResponseResult requestResult)
        {
            lock(_responseData)
            {
                _responseData.Add(requestResult);
            }
        }

        public bool UnitDataAmountIsEqualTo(int amount)
        {
            return _responseData.Count == amount;
        }

        public void OutputReport()
        {
            Console.WriteLine("===RESULT===");

        }
    }
}
