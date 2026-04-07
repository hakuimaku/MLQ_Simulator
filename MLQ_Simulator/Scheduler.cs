using System;
using System.Collections.Generic;
using System.Linq;

namespace MLQ_Simulator
{
    internal class Scheduler
    {
        public int TimeQuantum { get; set; }

        List<Process> ArrivalProcessList { get; set; } = new List<Process>();
        Queue<Process> RoundRobinQueue { get; set; }
        Queue<Process> SRTQueue { get; set; }
        Queue<Process> FCFSQueue { get; set; }

        public int CurrentTime { get; set; } = 0;
        public Process CurrentProcess { get; set; } = null;
        private int CurrentQuantumCounter = 0;
        private int CurrentProcessStartTime = 0; // Track when current process started running

        // Gantt chart tracking
        public List<GanttEntry> GanttHistory { get; private set; } = new List<GanttEntry>();

        public event EventHandler<string> OnLog;

        public Scheduler(int timeQuantum)
        {
            TimeQuantum = timeQuantum;
            RoundRobinQueue = new Queue<Process>();
            SRTQueue = new Queue<Process>();
            FCFSQueue = new Queue<Process>();
        }

        public void AddProcess(Process process)
        {
            ArrivalProcessList.Add(process);
            Log($"Process {process.Name} added to waiting list (Arrival: {process.ArrivalTime})");
        }

        public void LoadProcess()
        {
            for (int i = ArrivalProcessList.Count - 1; i >= 0; i--)
            {
                Process process = ArrivalProcessList[i];
                if (process.ArrivalTime <= CurrentTime)
                {
                    switch (process.QueueType)
                    {
                        case 1:
                            RoundRobinQueue.Enqueue(process);
                            Log($"[Time {CurrentTime}] Process {process.Name} arrived and added to Round Robin Queue");
                            break;
                        case 2:
                            SRTQueue.Enqueue(process);
                            Log($"[Time {CurrentTime}] Process {process.Name} arrived and added to SRT Queue (SRT - Burst: {process.RemainingTime})");
                            break;
                        case 3:
                            FCFSQueue.Enqueue(process);
                            Log($"[Time {CurrentTime}] Process {process.Name} arrived and added to FCFS Queue");
                            break;
                    }
                    ArrivalProcessList.RemoveAt(i);
                }
            }
        }

        public bool HasRemainingProcesses()
        {
            return ArrivalProcessList.Count > 0 || RoundRobinQueue.Count > 0 || SRTQueue.Count > 0 || FCFSQueue.Count > 0 || CurrentProcess != null;
        }

        private void SortSRTQueue()
        {
            if (SRTQueue.Count <= 1) return;

            List<Process> temp = new List<Process>();
            while (SRTQueue.Count > 0)
                temp.Add(SRTQueue.Dequeue());

            temp.Sort((p1, p2) => p1.RemainingTime.CompareTo(p2.RemainingTime));

            foreach (Process p in temp)
                SRTQueue.Enqueue(p);
        }

        private void AddGanttEntry(string processName, int startTime, int endTime, int queueType)
        {
            if (endTime > startTime)
            {
                GanttHistory.Add(new GanttEntry(processName, startTime, endTime, queueType));
            }
        }

        // Step-by-step execution: Execute 1 time unit
        public void Step()
        {
            LoadProcess();

            // Nếu không có process đang chạy, chọn process mới
            if (CurrentProcess == null)
            {
                if (RoundRobinQueue.Count == 0 && SRTQueue.Count == 0 && FCFSQueue.Count == 0)
                {
                    if (ArrivalProcessList.Count > 0)
                    {
                        Log($"[Time {CurrentTime}] CPU Idle - Waiting for process arrival");
                        AddGanttEntry("IDLE", CurrentTime, CurrentTime + 1, 0);
                        CurrentTime++;
                    }
                    return;
                }

                // Chọn process theo priority
                if (RoundRobinQueue.Count > 0)
                {
                    CurrentProcess = RoundRobinQueue.Dequeue();
                    CurrentQuantumCounter = 0;
                    CurrentProcessStartTime = CurrentTime;
                    if (CurrentProcess.FirstRunTime == -1)
                    {
                        CurrentProcess.FirstRunTime = CurrentTime;
                        CurrentProcess.ResponseTime = CurrentProcess.FirstRunTime - CurrentProcess.ArrivalTime;
                    }
                    Log($"[Time {CurrentTime}] Running {CurrentProcess.Name} from Round Robin Queue");
                }
                else if (SRTQueue.Count > 0)
                {
                    SortSRTQueue();
                    CurrentProcess = SRTQueue.Dequeue();
                    CurrentProcessStartTime = CurrentTime;
                    if (CurrentProcess.FirstRunTime == -1)
                    {
                        CurrentProcess.FirstRunTime = CurrentTime;
                        CurrentProcess.ResponseTime = CurrentProcess.FirstRunTime - CurrentProcess.ArrivalTime;
                    }
                    Log($"[Time {CurrentTime}] Running {CurrentProcess.Name} from SRT Queue (SRT - Remaining: {CurrentProcess.RemainingTime})");
                }
                else if (FCFSQueue.Count > 0)
                {
                    CurrentProcess = FCFSQueue.Dequeue();
                    CurrentProcessStartTime = CurrentTime;
                    if (CurrentProcess.FirstRunTime == -1)
                    {
                        CurrentProcess.FirstRunTime = CurrentTime;
                        CurrentProcess.ResponseTime = CurrentProcess.FirstRunTime - CurrentProcess.ArrivalTime;
                    }
                    Log($"[Time {CurrentTime}] Running {CurrentProcess.Name} from FCFS Queue");
                }
            }

            // Execute 1 time unit
            if (CurrentProcess != null)
            {
                CurrentProcess.RemainingTime--;
                CurrentQuantumCounter++;
                CurrentTime++;
                LoadProcess();

                // Check completion
                if (CurrentProcess.RemainingTime == 0)
                {
                    CurrentProcess.IsCompleted = true;
                    CurrentProcess.CompletionTime = CurrentTime;
                    CurrentProcess.TurnaroundTime = CurrentProcess.CompletionTime - CurrentProcess.ArrivalTime;
                    CurrentProcess.WaitingTime = CurrentProcess.TurnaroundTime - CurrentProcess.BurstTime;

                    AddGanttEntry(CurrentProcess.Name, CurrentProcessStartTime, CurrentTime, CurrentProcess.QueueType);

                    Log($"[Time {CurrentTime}] Completed {CurrentProcess.Name} " +
                        $"| Response Time: {CurrentProcess.ResponseTime} " +
                        $"| Waiting Time: {CurrentProcess.WaitingTime} " +
                        $"| Turnaround Time: {CurrentProcess.TurnaroundTime}");
                    CurrentProcess = null;
                    CurrentQuantumCounter = 0;
                    return;
                }

                // Check preemption và time quantum
                int currentQueueType = GetCurrentProcessQueueType();

                // Round Robin Queue - Round Robin
                if (currentQueueType == 1 && CurrentQuantumCounter >= TimeQuantum)
                {
                    AddGanttEntry(CurrentProcess.Name, CurrentProcessStartTime, CurrentTime, CurrentProcess.QueueType);

                    Log($"[Time {CurrentTime}] Time slice expired for {CurrentProcess.Name} from Round Robin Queue | Remaining Time {CurrentProcess.RemainingTime}");
                    RoundRobinQueue.Enqueue(CurrentProcess);
                    CurrentProcess = null;
                    CurrentQuantumCounter = 0;
                }
                // SRT Queue - Preempted by System
                else if (currentQueueType == 2 && RoundRobinQueue.Count > 0)
                {
                    AddGanttEntry(CurrentProcess.Name, CurrentProcessStartTime, CurrentTime, CurrentProcess.QueueType);

                    Log($"[Time {CurrentTime}] Preempting {CurrentProcess.Name} from SRT Queue (System process arrived) | Remaining Time: {CurrentProcess.RemainingTime}");
                    SRTQueue.Enqueue(CurrentProcess);
                    CurrentProcess = null;
                }
                // FCFS Queue - Preempted by System or Interactive
                else if (currentQueueType == 3 && (RoundRobinQueue.Count > 0 || SRTQueue.Count > 0))
                {
                    AddGanttEntry(CurrentProcess.Name, CurrentProcessStartTime, CurrentTime, CurrentProcess.QueueType);

                    string reason = RoundRobinQueue.Count > 0 ? "RR" : "SRT";
                    Log($"[Time {CurrentTime}] Preempting {CurrentProcess.Name} from FCFS Queue ({reason} process arrived) | Remaining Time: {CurrentProcess.RemainingTime}");
                    FCFSQueue.Enqueue(CurrentProcess);
                    CurrentProcess = null;
                }
            }
        }

        private int GetCurrentProcessQueueType()
        {
            if (CurrentProcess == null) return 0;
            return CurrentProcess.QueueType;
        }

        public List<Process> GetRoundRobinQueueList()
        {
            return RoundRobinQueue.ToList();
        }

        public List<Process> GetSRTQueueList()
        {
            return SRTQueue.ToList();
        }

        public List<Process> GetFCFSQueueList()
        {
            return FCFSQueue.ToList();
        }

        public List<Process> GetWaitingList()
        {
            return ArrivalProcessList.ToList();
        }

        private void Log(string message)
        {
            OnLog?.Invoke(this, message);
        }

        public void Run()
        {
            while (HasRemainingProcesses())
            {
                LoadProcess();

                if (RoundRobinQueue.Count == 0 && SRTQueue.Count == 0 && FCFSQueue.Count == 0)
                {
                    if (ArrivalProcessList.Count > 0)
                    {
                        Console.WriteLine($"[Time {CurrentTime}] CPU Idle - Waiting for process arrival");
                        CurrentTime++;
                    }
                    continue;
                }

                if (RoundRobinQueue.Count > 0)
                {
                    CurrentProcess = RoundRobinQueue.Dequeue();

                    if (CurrentProcess.FirstRunTime == -1)
                    {
                        CurrentProcess.FirstRunTime = CurrentTime;
                        CurrentProcess.ResponseTime = CurrentProcess.FirstRunTime - CurrentProcess.ArrivalTime;
                    }

                    Console.WriteLine($"[Time {CurrentTime}] Running {CurrentProcess.Name} from Round Robin Queue");
                    CurrentQuantumCounter = 0;
                    while (CurrentProcess.RemainingTime > 0 && CurrentQuantumCounter < TimeQuantum)
                    {
                        CurrentProcess.RemainingTime--;
                        CurrentQuantumCounter++;
                        CurrentTime++;
                        LoadProcess();
                    }
                    if (CurrentProcess.RemainingTime > 0)
                    {
                        Console.WriteLine($"[Time {CurrentTime}] Time slice expired for {CurrentProcess.Name} from Round Robin Queue | Remaining Time {CurrentProcess.RemainingTime}");
                        RoundRobinQueue.Enqueue(CurrentProcess);
                    }
                    else
                    {
                        CurrentProcess.IsCompleted = true;
                        CurrentProcess.CompletionTime = CurrentTime;
                        CurrentProcess.TurnaroundTime = CurrentProcess.CompletionTime - CurrentProcess.ArrivalTime;
                        CurrentProcess.WaitingTime = CurrentProcess.TurnaroundTime - CurrentProcess.BurstTime;
                        Console.WriteLine($"[Time {CurrentTime}] Completed {CurrentProcess.Name} from Round Robin Queue " +
                            $"| Response Time: {CurrentProcess.ResponseTime} " +
                            $"| Waiting Time: {CurrentProcess.WaitingTime} " +
                            $"| Turnaround Time: {CurrentProcess.TurnaroundTime}");
                    }
                }
                else if (SRTQueue.Count > 0)
                {
                    SortSRTQueue();
                    CurrentProcess = SRTQueue.Dequeue();

                    if (CurrentProcess.FirstRunTime == -1)
                    {
                        CurrentProcess.FirstRunTime = CurrentTime;
                        CurrentProcess.ResponseTime = CurrentProcess.FirstRunTime - CurrentProcess.ArrivalTime;
                    }

                    Console.WriteLine($"[Time {CurrentTime}] Running {CurrentProcess.Name} from SRT Queue (SRT - Remaining: {CurrentProcess.RemainingTime})");
                    bool preempted = false;
                    while (CurrentProcess.RemainingTime > 0)
                    {
                        CurrentProcess.RemainingTime--;
                        CurrentTime++;
                        LoadProcess();

                        if (RoundRobinQueue.Count > 0)
                        {
                            Console.WriteLine($"[Time {CurrentTime}] Preempting {CurrentProcess.Name} from SRT Queue (System process arrived) | Remaining Time: {CurrentProcess.RemainingTime}");
                            SRTQueue.Enqueue(CurrentProcess);
                            preempted = true;
                            break;
                        }
                        SortSRTQueue();
                        if (SRTQueue.Count > 0 && SRTQueue.Peek().RemainingTime < CurrentProcess.RemainingTime)
                        {
                            Console.WriteLine($"[Time {CurrentTime}] Preempting {CurrentProcess.Name} from SRT Queue (shorter SRT arrived, selected {SRTQueue.Peek().Name}) | Remaining Time: {CurrentProcess.RemainingTime}");
                            SRTQueue.Enqueue(CurrentProcess);
                            preempted = true;
                            break;
                        }
                    }

                    if (!preempted)
                    {
                        CurrentProcess.IsCompleted = true;
                        CurrentProcess.CompletionTime = CurrentTime;
                        CurrentProcess.TurnaroundTime = CurrentProcess.CompletionTime - CurrentProcess.ArrivalTime;
                        CurrentProcess.WaitingTime = CurrentProcess.TurnaroundTime - CurrentProcess.BurstTime;
                        Console.WriteLine($"[Time {CurrentTime}] Completed {CurrentProcess.Name} from SRT Queue " +
                                $"| Response Time: {CurrentProcess.ResponseTime} " +
                                $"| Waiting Time: {CurrentProcess.WaitingTime} " +
                                $"| Turnaround Time: {CurrentProcess.TurnaroundTime}");
                    }
                }
                else if (FCFSQueue.Count > 0)
                {
                    CurrentProcess = FCFSQueue.Dequeue();

                    if (CurrentProcess.FirstRunTime == -1)
                    {
                        CurrentProcess.FirstRunTime = CurrentTime;
                        CurrentProcess.ResponseTime = CurrentProcess.FirstRunTime - CurrentProcess.ArrivalTime;
                    }

                    Console.WriteLine($"[Time {CurrentTime}] Running {CurrentProcess.Name} from FCFS Queue");
                    bool preempted = false;
                    while (CurrentProcess.RemainingTime > 0)
                    {
                        CurrentProcess.RemainingTime--;
                        CurrentTime++;
                        LoadProcess();

                        if (RoundRobinQueue.Count > 0)
                        {
                            Console.WriteLine($"[Time {CurrentTime}] Preempting {CurrentProcess.Name} from FCFS Queue (RR process arrived) | Remaining Time: {CurrentProcess.RemainingTime}");
                            FCFSQueue.Enqueue(CurrentProcess);
                            preempted = true;
                            break;
                        }
                        else if (SRTQueue.Count > 0)
                        {
                            Console.WriteLine($"[Time {CurrentTime}] Preempting {CurrentProcess.Name} from FCFS Queue (SRT process arrived) | Remaining Time: {CurrentProcess.RemainingTime}");
                            FCFSQueue.Enqueue(CurrentProcess);
                            preempted = true;
                            break;
                        }
                    }

                    if (!preempted)
                    {
                        CurrentProcess.IsCompleted = true;
                        CurrentProcess.CompletionTime = CurrentTime;
                        CurrentProcess.TurnaroundTime = CurrentProcess.CompletionTime - CurrentProcess.ArrivalTime;
                        CurrentProcess.WaitingTime = CurrentProcess.TurnaroundTime - CurrentProcess.BurstTime;
                        Console.WriteLine($"[Time {CurrentTime}] Completed {CurrentProcess.Name} from FCFS Queue " +
                                $"| Response Time: {CurrentProcess.ResponseTime} " +
                                $"| Waiting Time: {CurrentProcess.WaitingTime} " +
                                $"| Turnaround Time: {CurrentProcess.TurnaroundTime}");
                    }
                }
            }
        }
    }
}
