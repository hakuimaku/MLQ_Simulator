using System;

namespace MLQ_Simulator
{
    public class Process
    {
        // Các thuộc tính cơ bản của tiến trình
        public string PID { get; set; }
        public string Name { get; set; }
        public int ArrivalTime { get; set; }
        public int BurstTime { get; set; }

        public int QueueType { get; set; }

        // Các thuộc tính để tính toán thời gian
        public int RemainingTime { get; set; } // Thời gian còn lại để hoàn thành tiến trình (dành cho RR và SJF)
        public int CompletionTime { get; set; } // Thời gian hoàn thành của tiến trình (CurrentTime khi tiến trình hoàn thành)
        public int WaitingTime { get; set; } // Thời gian chờ đợi trong hàng đợi (CompletionTime - ArrivalTime - BurstTime)
        public int TurnaroundTime { get; set; } // Thời gian lưu lại hệ thống (CompletionTime - ArrivalTime)
        public int ResponseTime { get; set; } // Thời gian phản hồi (FirstRunTime - ArrivalTime)
        public int FirstRunTime { get; set; } = -1; // Thời điểm lần đầu process được chạy (-1 = chưa chạy)
        public bool IsCompleted { get; set; } = false;

        public Process(string PID, string Name, int ArrivalTime, int BurstTime, int QueueType)
        {
            this.PID = PID;
            this.Name = Name;
            this.ArrivalTime = ArrivalTime;
            this.BurstTime = BurstTime;
            this.QueueType = QueueType;
            this.RemainingTime = BurstTime; // Ban đầu RemainingTime bằng BurstTime
        }

        public Process() { }

        public void Display()
        {
            Console.WriteLine($"{PID,10}{Name,30}{ArrivalTime,20}{BurstTime,20}{QueueType,20}");
        }

        public void CompleteDisplay()
        {
            Console.WriteLine($"{PID,10}{Name,30}{ArrivalTime,20}{BurstTime,20}{CompletionTime,20}{WaitingTime,20}{TurnaroundTime,20}");
        }
    }

}
