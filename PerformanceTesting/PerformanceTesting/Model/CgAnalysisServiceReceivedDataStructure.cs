namespace PerformanceTesting.Model
{
    /// <summary>
    /// Format that will be sent to CgAnalysis service.
    /// </summary>
    class CgAnalysisServiceReceivedDataStructure
    {
        public string ElasticsearchIndexName { get; set; }
        public string DeviceUID { get; set; }
        public bool IsSimulator { get; set; }
    }
}
