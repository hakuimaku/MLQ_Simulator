using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MLQ_Simulator
{
    public partial class Form1 : Form
    {
        private Scheduler scheduler;
        private int processCounter = 1;
        private Dictionary<string, Color> processColors = new Dictionary<string, Color>();
        private Color[] colorPalette = new Color[]
        {
            Color.FromArgb(255, 99, 132),   // Red
            Color.FromArgb(54, 162, 235),   // Blue
            Color.FromArgb(255, 206, 86),   // Yellow
            Color.FromArgb(75, 192, 192),   // Teal
            Color.FromArgb(153, 102, 255),  // Purple
            Color.FromArgb(255, 159, 64),   // Orange
            Color.FromArgb(199, 199, 199),  // Gray
            Color.FromArgb(83, 102, 255),   // Indigo
            Color.FromArgb(255, 99, 255),   // Pink
            Color.FromArgb(99, 255, 132)    // Green
        };
        private int colorIndex = 0;
        private float zoomLevel = 1.0f;
        private readonly Random random = new Random();



        private readonly Dictionary<int, int> randomProcessOrderByQueue = new Dictionary<int, int>
        {
            { 1, 1 }, // Round Robin
            { 2, 1 }, // SRT
            { 3, 1 }  // FCFS
        };

        public Form1()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;

            cmbQueueType.SelectedIndex = 0;
            UpdateUIWhenNoScheduler();
        }

        private void InitializeScheduler()
        {
            int timeQuantum = 4;
            if (!int.TryParse(txtTimeQuantum.Text, out timeQuantum) || timeQuantum <= 0)
            {
                MessageBox.Show("Please enter a valid Time Quantum (> 0)", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTimeQuantum.Text = "4";
                timeQuantum = 4;
            }

            scheduler = new Scheduler(timeQuantum);
            scheduler.OnLog += Scheduler_OnLog;

            UpdateUI();
            AddLog("Scheduler initialized with Time Quantum = " + timeQuantum);
        }

        private void Scheduler_OnLog(object sender, string e)
        {
            AddLog(e);
        }

        private Color GetQueueColor(int queueType)
        {
            switch (queueType)
            {
                case 1: // Round Robin
                    return Color.Red;
                case 2: // SRT
                    return Color.RoyalBlue;
                case 3: // FCFS
                    return Color.Yellow;
                default:
                    return Color.Gray;
            }
        }

        // Nút thêm process thủ công
        private void btnAddProcess_Click(object sender, EventArgs e)
        {
            try
            {
                if (scheduler == null)
                {
                    InitializeScheduler();
                }

                string name = txtProcessName.Text.Trim();
                if (string.IsNullOrEmpty(name))
                    name = "P" + processCounter;

                int arrivalTime = int.Parse(txtArrivalTime.Text);
                int burstTime = int.Parse(txtBurstTime.Text);
                int queueType = cmbQueueType.SelectedIndex + 1;

                string pid = "P" + processCounter++;
                Process process = new Process(pid, name, arrivalTime, burstTime, queueType);

                scheduler.AddProcess(process);

                txtProcessName.Clear();
                txtArrivalTime.Clear();
                txtBurstTime.Clear();

                UpdateUI();

                txtTimeQuantum.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Nút tạo process ngẫu nhiên
        private void btnAddRandomProcess_Click(object sender, EventArgs e)
        {
            try
            {
                if (scheduler == null)
                {
                    InitializeScheduler();
                }

                int currentTime = scheduler != null ? scheduler.CurrentTime : 0;

                int arrivalTime = random.Next(currentTime + 1, currentTime + 11);

                int burstTime = random.Next(4, 21);

                int queueType = cmbQueueType.SelectedIndex + 1;

                string pid = "P" + processCounter++;
                string name = GetRandomProcessName(queueType);

                Process process = new Process(pid, name, arrivalTime, burstTime, queueType);
                scheduler.AddProcess(process);

                AddLog($"Random process created: {name} | Arrival: {arrivalTime} | Burst: {burstTime} | Queue: {GetQueueName(queueType)}");

                UpdateUI();
                txtTimeQuantum.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Random Process Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnStep_Click(object sender, EventArgs e)
        {
            if (scheduler == null)
            {
                MessageBox.Show("Please add at least one process first.", "No Process", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (scheduler.HasRemainingProcesses())
            {
                scheduler.Step();
                UpdateUI();
                panelGantt.Invalidate();
            }
            else
            {
                MessageBox.Show("Simulation completed! All processes have finished.", "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            processCounter = 1;
            colorIndex = 0;
            processColors.Clear();
            txtLog.Clear();
            scheduler = null;
            txtTimeQuantum.Enabled = true;
            zoomLevel = 1.0f;
            trackBarZoom.Value = 100;
            lblZoomValue.Text = "100%";
            UpdateUIWhenNoScheduler();
            panelGantt.Invalidate();
            AddLog("System reset. Please set Time Quantum and add processes.");

            randomProcessOrderByQueue[1] = 1;
            randomProcessOrderByQueue[2] = 1;
            randomProcessOrderByQueue[3] = 1;
        }

        private void UpdateUI()
        {
            if (scheduler == null) return;

            lblCurrentTime.Text = "Current Time: " + scheduler.CurrentTime;

            if (scheduler.CurrentProcess != null)
            {
                lblCurrentProcessInfo.Text = $"{scheduler.CurrentProcess.Name} | Queue: {GetQueueName(scheduler.CurrentProcess.QueueType)} | Remaining: {scheduler.CurrentProcess.RemainingTime}";
                lblCurrentProcessInfo.ForeColor = Color.Green;
            }
            else
            {
                lblCurrentProcessInfo.Text = "CPU is IDLE";
                lblCurrentProcessInfo.ForeColor = Color.Red;
            }

            UpdateQueueDisplay(lstWaiting, scheduler.GetWaitingList());
            UpdateQueueDisplay(lstRoundRobinQueue, scheduler.GetRoundRobinQueueList());
            UpdateQueueDisplay(lstSRTQueue, scheduler.GetSRTQueueList());
            UpdateQueueDisplay(lstFCFSQueue, scheduler.GetFCFSQueueList());

            lblRoundRobinQueue.Text = $"Round Robin Queue (TQ={scheduler.TimeQuantum}):";
        }

        private void UpdateUIWhenNoScheduler()
        {
            lblCurrentTime.Text = "Current Time: 0";
            lblCurrentProcessInfo.Text = "Not Started";
            lblCurrentProcessInfo.ForeColor = Color.Gray;
            lstWaiting.Items.Clear();
            lstRoundRobinQueue.Items.Clear();
            lstSRTQueue.Items.Clear();
            lstFCFSQueue.Items.Clear();
            lblRoundRobinQueue.Text = "Round Robin Queue (TQ=?):";
        }

        private void UpdateQueueDisplay(ListBox listBox, List<Process> processes)
        {
            listBox.Items.Clear();
            foreach (var p in processes)
            {
                listBox.Items.Add($"{p.Name} (Arrival: {p.ArrivalTime}, Burst: {p.BurstTime}, Remaining: {p.RemainingTime})");
            }
        }

        private string GetQueueName(int queueType)
        {
            switch (queueType)
            {
                case 1: return "Round Robin Queue";
                case 2: return "SRT Queue";
                case 3: return "FCFS Queue";
                default: return "Unknown";
            }
        }

        private void AddLog(string message)
        {
            txtLog.AppendText(message + Environment.NewLine);
            txtLog.SelectionStart = txtLog.Text.Length;
            txtLog.ScrollToCaret();
        }
        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            if (trackBarZoom.Value < trackBarZoom.Maximum)
            {
                trackBarZoom.Value = Math.Min(trackBarZoom.Maximum, trackBarZoom.Value + 25);
                UpdateZoomLevel();
            }
        }

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            if (trackBarZoom.Value > trackBarZoom.Minimum)
            {
                trackBarZoom.Value = Math.Max(trackBarZoom.Minimum, trackBarZoom.Value - 25);
                UpdateZoomLevel();
            }
        }

        private void btnResetZoom_Click(object sender, EventArgs e)
        {
            trackBarZoom.Value = 100;
            UpdateZoomLevel();
        }

        private void trackBarZoom_Scroll(object sender, EventArgs e)
        {
            UpdateZoomLevel();
        }

        private void UpdateZoomLevel()
        {
            zoomLevel = trackBarZoom.Value / 100.0f;
            lblZoomValue.Text = trackBarZoom.Value + "%";
            panelGantt.Invalidate();
        }

        private void panelGantt_Paint(object sender, PaintEventArgs e)
        {
            if (scheduler == null || scheduler.GanttHistory.Count == 0)
            {
                e.Graphics.DrawString("No execution history yet",
                    new Font("Arial", 10),
                    Brushes.Gray,
                    new PointF(10, 10));
                return;
            }

            Graphics g = e.Graphics;

            // Chỉ lấy entry thực thi process, bỏ IDLE để tạo khoảng trống
            List<GanttEntry> runEntries = scheduler.GanttHistory
                .Where(x => !string.Equals(x.ProcessName, "IDLE", StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (runEntries.Count == 0)
            {
                e.Graphics.DrawString("CPU was idle",
                    new Font("Arial", 10),
                    Brushes.Gray,
                    new PointF(10, 10));
                return;
            }

            // Thứ tự process theo lần xuất hiện đầu tiên trên timeline
            List<string> processOrder = new List<string>();
            foreach (var entry in runEntries)
            {
                if (!processOrder.Contains(entry.ProcessName))
                    processOrder.Add(entry.ProcessName);
            }

            Dictionary<string, int> rowByProcess = new Dictionary<string, int>();
            for (int i = 0; i < processOrder.Count; i++)
                rowByProcess[processOrder[i]] = i;

            // Kích thước lưới
            int tickWidth = Math.Max(12, (int)(24 * zoomLevel));   // 1 cột = 1 tick
            int rowHeight = Math.Max(20, (int)(32 * zoomLevel));   // 1 dòng = 1 process
            int labelWidth = Math.Max(90, (int)(130 * zoomLevel));

            int left = labelWidth + 10;
            int top = 10;
            int totalTime = Math.Max(1, scheduler.CurrentTime);

            int canvasWidth = left + totalTime * tickWidth + 60;
            int canvasHeight = top + processOrder.Count * rowHeight + 60;

            if (panelGantt.AutoScrollMinSize.Width != canvasWidth ||
                panelGantt.AutoScrollMinSize.Height != canvasHeight)
            {
                panelGantt.AutoScrollMinSize = new Size(canvasWidth, canvasHeight);
            }

            g.TranslateTransform(panelGantt.AutoScrollPosition.X, panelGantt.AutoScrollPosition.Y);

            // Vẽ grid + nhãn trục dọc (process names)
            using (Pen gridPen = new Pen(Color.LightGray))
            using (Pen axisPen = new Pen(Color.Black))
            {
                // Horizontal lines + process labels
                for (int r = 0; r <= processOrder.Count; r++)
                {
                    int y = top + r * rowHeight;
                    g.DrawLine(gridPen, left, y, left + totalTime * tickWidth, y);

                    if (r < processOrder.Count)
                    {
                        string pname = processOrder[r];
                        g.DrawString(
                            pname,
                            new Font("Arial", Math.Max(7, 8 * zoomLevel), FontStyle.Bold),
                            Brushes.Black,
                            5,
                            y + (rowHeight - 14) / 2
                        );
                    }
                }

                // Vertical lines theo tick time
                for (int t = 0; t <= totalTime; t++)
                {
                    int x = left + t * tickWidth;
                    g.DrawLine(gridPen, x, top, x, top + processOrder.Count * rowHeight);
                }

                // Axis
                g.DrawRectangle(axisPen, left, top, totalTime * tickWidth, processOrder.Count * rowHeight);
            }

            // Vẽ block ngang theo timeline, luôn nằm đúng dòng process
            foreach (var entry in runEntries)
            {
                int row = rowByProcess[entry.ProcessName];
                int x = left + entry.StartTime * tickWidth;
                int y = top + row * rowHeight + 2;
                int w = Math.Max(1, (entry.EndTime - entry.StartTime) * tickWidth);
                int h = Math.Max(1, rowHeight - 4);

                Color color = GetQueueColor(entry.QueueType);

                using (SolidBrush brush = new SolidBrush(color))
                {
                    g.FillRectangle(brush, x, y, w, h);
                }
                g.DrawRectangle(Pens.Black, x, y, w, h);

                // Tên process trong block nếu đủ rộng
                if (w >= 28)
                {
                    StringFormat sf = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };

                    g.DrawString(
                        entry.ProcessName,
                        new Font("Arial", Math.Max(6, 7 * zoomLevel), FontStyle.Bold),
                        Brushes.Black,
                        new RectangleF(x, y, w, h),
                        sf
                    );
                }
            }

            // Nhãn trục ngang (time)
            int timelineY = top + processOrder.Count * rowHeight + 8;
            for (int t = 0; t <= totalTime; t++)
            {
                int x = left + t * tickWidth;
                g.DrawString(
                    t.ToString(),
                    new Font("Arial", Math.Max(6, 7 * zoomLevel)),
                    Brushes.Black,
                    x - 4,
                    timelineY
                );
            }

            g.DrawString(
                "Time",
                new Font("Arial", Math.Max(7, 8 * zoomLevel), FontStyle.Bold),
                Brushes.Black,
                left + totalTime * tickWidth + 8,
                timelineY
            );
        }

        private string GetRandomProcessName(int queueType)
        {
            string prefix;
            switch (queueType)
            {
                case 1: prefix = "RR"; break;
                case 2: prefix = "SRT"; break;
                case 3: prefix = "FCFS"; break;
                default: prefix = "Q"; break;
            }

            int order = randomProcessOrderByQueue[queueType];
            randomProcessOrderByQueue[queueType] = order + 1;

            return prefix + "_" + order;
        }
    }
}
