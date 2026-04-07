namespace MLQ_Simulator
{
    public class GanttEntry
    {
        public string ProcessName { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public int QueueType { get; set; }

        public GanttEntry(string processName, int startTime, int endTime, int queueType)
        {
            ProcessName = processName;
            StartTime = startTime;
            EndTime = endTime;
            QueueType = queueType;
        }
    }
}