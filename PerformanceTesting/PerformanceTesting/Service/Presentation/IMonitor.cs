namespace PerformanceTesting.Service.Presentation
{
    interface IMonitor
    {
        void OutputReport();
        void SaveEmptyResult(ResponseResult result);
        void SaveReceivedResult(ResponseResult result);
        bool UnitDataAmountIsEqualTo(int amount);
    }
}