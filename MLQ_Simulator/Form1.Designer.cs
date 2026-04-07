namespace MLQ_Simulator
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            grpInput = new GroupBox();
            btnAddRandomProcess = new Button();
            btnAddProcess = new Button();
            cmbQueueType = new ComboBox();
            txtBurstTime = new TextBox();
            txtArrivalTime = new TextBox();
            txtProcessName = new TextBox();
            lblQueueType = new Label();
            lblBurstTime = new Label();
            lblArrivalTime = new Label();
            lblProcessName = new Label();
            grpControl = new GroupBox();
            btnReset = new Button();
            btnStep = new Button();
            txtTimeQuantum = new TextBox();
            lblTimeQuantum = new Label();
            lblCurrentTime = new Label();
            tabControl = new TabControl();
            tabQueues = new TabPage();
            grpCurrentProcess = new GroupBox();
            lblCurrentProcessInfo = new Label();
            grpLog = new GroupBox();
            txtLog = new TextBox();
            grpQueues = new GroupBox();
            lstFCFSQueue = new ListBox();
            lstSRTQueue = new ListBox();
            lstRoundRobinQueue = new ListBox();
            lstWaiting = new ListBox();
            lblFCFSQueue = new Label();
            lblSRTQueue = new Label();
            lblRoundRobinQueue = new Label();
            lblWaiting = new Label();
            tabGantt = new TabPage();
            grpZoom = new GroupBox();
            lblZoomValue = new Label();
            trackBarZoom = new TrackBar();
            btnZoomOut = new Button();
            btnZoomIn = new Button();
            btnResetZoom = new Button();
            panelGantt = new Panel();
            grpInput.SuspendLayout();
            grpControl.SuspendLayout();
            tabControl.SuspendLayout();
            tabQueues.SuspendLayout();
            grpCurrentProcess.SuspendLayout();
            grpLog.SuspendLayout();
            grpQueues.SuspendLayout();
            tabGantt.SuspendLayout();
            grpZoom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarZoom).BeginInit();
            SuspendLayout();
            // 
            // grpInput
            // 
            grpInput.Controls.Add(btnAddRandomProcess);
            grpInput.Controls.Add(btnAddProcess);
            grpInput.Controls.Add(cmbQueueType);
            grpInput.Controls.Add(txtBurstTime);
            grpInput.Controls.Add(txtArrivalTime);
            grpInput.Controls.Add(txtProcessName);
            grpInput.Controls.Add(lblQueueType);
            grpInput.Controls.Add(lblBurstTime);
            grpInput.Controls.Add(lblArrivalTime);
            grpInput.Controls.Add(lblProcessName);
            grpInput.Location = new Point(16, 19);
            grpInput.Margin = new Padding(4, 5, 4, 5);
            grpInput.Name = "grpInput";
            grpInput.Padding = new Padding(4, 5, 4, 5);
            grpInput.Size = new Size(467, 308);
            grpInput.TabIndex = 0;
            grpInput.TabStop = false;
            grpInput.Text = "Add New Process";
            // 
            // btnAddRandomProcess
            // 
            btnAddRandomProcess.Location = new Point(27, 239);
            btnAddRandomProcess.Margin = new Padding(4, 5, 4, 5);
            btnAddRandomProcess.Name = "btnAddRandomProcess";
            btnAddRandomProcess.Size = new Size(191, 46);
            btnAddRandomProcess.TabIndex = 9;
            btnAddRandomProcess.Text = "Add Random Process";
            btnAddRandomProcess.UseVisualStyleBackColor = true;
            btnAddRandomProcess.Click += btnAddRandomProcess_Click;
            // 
            // btnAddProcess
            // 
            btnAddProcess.Location = new Point(292, 239);
            btnAddProcess.Margin = new Padding(4, 5, 4, 5);
            btnAddProcess.Name = "btnAddProcess";
            btnAddProcess.Size = new Size(133, 46);
            btnAddProcess.TabIndex = 8;
            btnAddProcess.Text = "Add Process";
            btnAddProcess.UseVisualStyleBackColor = true;
            btnAddProcess.Click += btnAddProcess_Click;
            // 
            // cmbQueueType
            // 
            cmbQueueType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbQueueType.FormattingEnabled = true;
            cmbQueueType.Items.AddRange(new object[] { "1 - Round Robin Queue", "2 - SRT Queue", "3 - FCFS Queue" });
            cmbQueueType.Location = new Point(160, 185);
            cmbQueueType.Margin = new Padding(4, 5, 4, 5);
            cmbQueueType.Name = "cmbQueueType";
            cmbQueueType.Size = new Size(265, 28);
            cmbQueueType.TabIndex = 7;
            // 
            // txtBurstTime
            // 
            txtBurstTime.Location = new Point(160, 139);
            txtBurstTime.Margin = new Padding(4, 5, 4, 5);
            txtBurstTime.Name = "txtBurstTime";
            txtBurstTime.Size = new Size(265, 27);
            txtBurstTime.TabIndex = 6;
            // 
            // txtArrivalTime
            // 
            txtArrivalTime.Location = new Point(160, 92);
            txtArrivalTime.Margin = new Padding(4, 5, 4, 5);
            txtArrivalTime.Name = "txtArrivalTime";
            txtArrivalTime.Size = new Size(265, 27);
            txtArrivalTime.TabIndex = 5;
            // 
            // txtProcessName
            // 
            txtProcessName.Location = new Point(160, 46);
            txtProcessName.Margin = new Padding(4, 5, 4, 5);
            txtProcessName.Name = "txtProcessName";
            txtProcessName.Size = new Size(265, 27);
            txtProcessName.TabIndex = 4;
            // 
            // lblQueueType
            // 
            lblQueueType.AutoSize = true;
            lblQueueType.Location = new Point(27, 189);
            lblQueueType.Margin = new Padding(4, 0, 4, 0);
            lblQueueType.Name = "lblQueueType";
            lblQueueType.Size = new Size(90, 20);
            lblQueueType.TabIndex = 3;
            lblQueueType.Text = "Queue Type:";
            // 
            // lblBurstTime
            // 
            lblBurstTime.AutoSize = true;
            lblBurstTime.Location = new Point(27, 142);
            lblBurstTime.Margin = new Padding(4, 0, 4, 0);
            lblBurstTime.Name = "lblBurstTime";
            lblBurstTime.Size = new Size(82, 20);
            lblBurstTime.TabIndex = 2;
            lblBurstTime.Text = "Burst Time:";
            // 
            // lblArrivalTime
            // 
            lblArrivalTime.AutoSize = true;
            lblArrivalTime.Location = new Point(27, 98);
            lblArrivalTime.Margin = new Padding(4, 0, 4, 0);
            lblArrivalTime.Name = "lblArrivalTime";
            lblArrivalTime.Size = new Size(92, 20);
            lblArrivalTime.TabIndex = 1;
            lblArrivalTime.Text = "Arrival Time:";
            // 
            // lblProcessName
            // 
            lblProcessName.AutoSize = true;
            lblProcessName.Location = new Point(27, 51);
            lblProcessName.Margin = new Padding(4, 0, 4, 0);
            lblProcessName.Name = "lblProcessName";
            lblProcessName.Size = new Size(105, 20);
            lblProcessName.TabIndex = 0;
            lblProcessName.Text = "Process Name:";
            // 
            // grpControl
            // 
            grpControl.Controls.Add(btnReset);
            grpControl.Controls.Add(btnStep);
            grpControl.Controls.Add(txtTimeQuantum);
            grpControl.Controls.Add(lblTimeQuantum);
            grpControl.Controls.Add(lblCurrentTime);
            grpControl.Location = new Point(16, 335);
            grpControl.Margin = new Padding(4, 5, 4, 5);
            grpControl.Name = "grpControl";
            grpControl.Padding = new Padding(4, 5, 4, 5);
            grpControl.Size = new Size(467, 231);
            grpControl.TabIndex = 1;
            grpControl.TabStop = false;
            grpControl.Text = "Simulation Control";
            // 
            // btnReset
            // 
            btnReset.Location = new Point(293, 154);
            btnReset.Margin = new Padding(4, 5, 4, 5);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(133, 54);
            btnReset.TabIndex = 4;
            btnReset.Text = "Reset";
            btnReset.UseVisualStyleBackColor = true;
            btnReset.Click += btnReset_Click;
            // 
            // btnStep
            // 
            btnStep.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnStep.Location = new Point(27, 154);
            btnStep.Margin = new Padding(4, 5, 4, 5);
            btnStep.Name = "btnStep";
            btnStep.Size = new Size(240, 54);
            btnStep.TabIndex = 3;
            btnStep.Text = "Next Step ►";
            btnStep.UseVisualStyleBackColor = true;
            btnStep.Click += btnStep_Click;
            // 
            // txtTimeQuantum
            // 
            txtTimeQuantum.Location = new Point(160, 92);
            txtTimeQuantum.Margin = new Padding(4, 5, 4, 5);
            txtTimeQuantum.Name = "txtTimeQuantum";
            txtTimeQuantum.Size = new Size(132, 27);
            txtTimeQuantum.TabIndex = 2;
            // 
            // lblTimeQuantum
            // 
            lblTimeQuantum.AutoSize = true;
            lblTimeQuantum.Location = new Point(27, 98);
            lblTimeQuantum.Margin = new Padding(4, 0, 4, 0);
            lblTimeQuantum.Name = "lblTimeQuantum";
            lblTimeQuantum.Size = new Size(110, 20);
            lblTimeQuantum.TabIndex = 1;
            lblTimeQuantum.Text = "Time Quantum:";
            // 
            // lblCurrentTime
            // 
            lblCurrentTime.AutoSize = true;
            lblCurrentTime.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCurrentTime.ForeColor = Color.Blue;
            lblCurrentTime.Location = new Point(27, 39);
            lblCurrentTime.Margin = new Padding(4, 0, 4, 0);
            lblCurrentTime.Name = "lblCurrentTime";
            lblCurrentTime.Size = new Size(194, 29);
            lblCurrentTime.TabIndex = 0;
            lblCurrentTime.Text = "Current Time: 0";
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabQueues);
            tabControl.Controls.Add(tabGantt);
            tabControl.Location = new Point(491, 19);
            tabControl.Margin = new Padding(4, 5, 4, 5);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(1400, 1186);
            tabControl.TabIndex = 2;
            // 
            // tabQueues
            // 
            tabQueues.Controls.Add(grpCurrentProcess);
            tabQueues.Controls.Add(grpLog);
            tabQueues.Controls.Add(grpQueues);
            tabQueues.Location = new Point(4, 29);
            tabQueues.Margin = new Padding(4, 5, 4, 5);
            tabQueues.Name = "tabQueues";
            tabQueues.Padding = new Padding(4, 5, 4, 5);
            tabQueues.Size = new Size(1392, 1153);
            tabQueues.TabIndex = 0;
            tabQueues.Text = "Queues & Logs";
            tabQueues.UseVisualStyleBackColor = true;
            // 
            // grpCurrentProcess
            // 
            grpCurrentProcess.Controls.Add(lblCurrentProcessInfo);
            grpCurrentProcess.Location = new Point(8, 439);
            grpCurrentProcess.Margin = new Padding(4, 5, 4, 5);
            grpCurrentProcess.Name = "grpCurrentProcess";
            grpCurrentProcess.Padding = new Padding(4, 5, 4, 5);
            grpCurrentProcess.Size = new Size(902, 108);
            grpCurrentProcess.TabIndex = 2;
            grpCurrentProcess.TabStop = false;
            grpCurrentProcess.Text = "Current Running Process";
            // 
            // lblCurrentProcessInfo
            // 
            lblCurrentProcessInfo.AutoSize = true;
            lblCurrentProcessInfo.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCurrentProcessInfo.ForeColor = Color.Green;
            lblCurrentProcessInfo.Location = new Point(27, 46);
            lblCurrentProcessInfo.Margin = new Padding(4, 0, 4, 0);
            lblCurrentProcessInfo.Name = "lblCurrentProcessInfo";
            lblCurrentProcessInfo.Size = new Size(111, 24);
            lblCurrentProcessInfo.TabIndex = 0;
            lblCurrentProcessInfo.Text = "CPU is IDLE";
            // 
            // grpLog
            // 
            grpLog.Controls.Add(txtLog);
            grpLog.Location = new Point(8, 554);
            grpLog.Margin = new Padding(4, 5, 4, 5);
            grpLog.Name = "grpLog";
            grpLog.Padding = new Padding(4, 5, 4, 5);
            grpLog.Size = new Size(902, 559);
            grpLog.TabIndex = 1;
            grpLog.TabStop = false;
            grpLog.Text = "Execution Log";
            // 
            // txtLog
            // 
            txtLog.BackColor = Color.Black;
            txtLog.Font = new Font("Consolas", 13F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtLog.ForeColor = Color.Lime;
            txtLog.Location = new Point(8, 30);
            txtLog.Margin = new Padding(4, 5, 4, 5);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ReadOnly = true;
            txtLog.ScrollBars = ScrollBars.Vertical;
            txtLog.Size = new Size(894, 375);
            txtLog.TabIndex = 0;
            // 
            // grpQueues
            // 
            grpQueues.Controls.Add(lstFCFSQueue);
            grpQueues.Controls.Add(lstSRTQueue);
            grpQueues.Controls.Add(lstRoundRobinQueue);
            grpQueues.Controls.Add(lstWaiting);
            grpQueues.Controls.Add(lblFCFSQueue);
            grpQueues.Controls.Add(lblSRTQueue);
            grpQueues.Controls.Add(lblRoundRobinQueue);
            grpQueues.Controls.Add(lblWaiting);
            grpQueues.Location = new Point(8, 9);
            grpQueues.Margin = new Padding(4, 5, 4, 5);
            grpQueues.Name = "grpQueues";
            grpQueues.Padding = new Padding(4, 5, 4, 5);
            grpQueues.Size = new Size(902, 420);
            grpQueues.TabIndex = 0;
            grpQueues.TabStop = false;
            grpQueues.Text = "Process Queues";
            // 
            // lstFCFSQueue
            // 
            lstFCFSQueue.FormattingEnabled = true;
            lstFCFSQueue.Location = new Point(452, 224);
            lstFCFSQueue.Margin = new Padding(4, 5, 4, 5);
            lstFCFSQueue.Name = "lstFCFSQueue";
            lstFCFSQueue.Size = new Size(436, 144);
            lstFCFSQueue.TabIndex = 7;
            // 
            // lstSRTQueue
            // 
            lstSRTQueue.FormattingEnabled = true;
            lstSRTQueue.Location = new Point(13, 224);
            lstSRTQueue.Margin = new Padding(4, 5, 4, 5);
            lstSRTQueue.Name = "lstSRTQueue";
            lstSRTQueue.Size = new Size(431, 144);
            lstSRTQueue.TabIndex = 5;
            // 
            // lstRoundRobinQueue
            // 
            lstRoundRobinQueue.FormattingEnabled = true;
            lstRoundRobinQueue.Location = new Point(452, 48);
            lstRoundRobinQueue.Margin = new Padding(4, 5, 4, 5);
            lstRoundRobinQueue.Name = "lstRoundRobinQueue";
            lstRoundRobinQueue.Size = new Size(436, 144);
            lstRoundRobinQueue.TabIndex = 6;
            // 
            // lstWaiting
            // 
            lstWaiting.FormattingEnabled = true;
            lstWaiting.Location = new Point(13, 48);
            lstWaiting.Margin = new Padding(4, 5, 4, 5);
            lstWaiting.Name = "lstWaiting";
            lstWaiting.Size = new Size(431, 144);
            lstWaiting.TabIndex = 4;
            // 
            // lblFCFSQueue
            // 
            lblFCFSQueue.AutoSize = true;
            lblFCFSQueue.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFCFSQueue.Location = new Point(434, 203);
            lblFCFSQueue.Margin = new Padding(4, 0, 4, 0);
            lblFCFSQueue.Name = "lblFCFSQueue";
            lblFCFSQueue.Size = new Size(110, 18);
            lblFCFSQueue.TabIndex = 3;
            lblFCFSQueue.Text = "FCFS Queue:";
            // 
            // lblSRTQueue
            // 
            lblSRTQueue.AutoSize = true;
            lblSRTQueue.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSRTQueue.Location = new Point(13, 203);
            lblSRTQueue.Margin = new Padding(4, 0, 4, 0);
            lblSRTQueue.Name = "lblSRTQueue";
            lblSRTQueue.Size = new Size(100, 18);
            lblSRTQueue.TabIndex = 2;
            lblSRTQueue.Text = "SRT Queue:";
            // 
            // lblRoundRobinQueue
            // 
            lblRoundRobinQueue.AutoSize = true;
            lblRoundRobinQueue.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblRoundRobinQueue.Location = new Point(452, 25);
            lblRoundRobinQueue.Margin = new Padding(4, 0, 4, 0);
            lblRoundRobinQueue.Name = "lblRoundRobinQueue";
            lblRoundRobinQueue.Size = new Size(224, 18);
            lblRoundRobinQueue.TabIndex = 1;
            lblRoundRobinQueue.Text = "Round Robin Queue (TQ=2):";
            // 
            // lblWaiting
            // 
            lblWaiting.AutoSize = true;
            lblWaiting.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblWaiting.Location = new Point(13, 26);
            lblWaiting.Margin = new Padding(4, 0, 4, 0);
            lblWaiting.Name = "lblWaiting";
            lblWaiting.Size = new Size(147, 18);
            lblWaiting.TabIndex = 0;
            lblWaiting.Text = "Waiting for Arrival:";
            // 
            // tabGantt
            // 
            tabGantt.Controls.Add(grpZoom);
            tabGantt.Controls.Add(panelGantt);
            tabGantt.Location = new Point(4, 29);
            tabGantt.Margin = new Padding(4, 5, 4, 5);
            tabGantt.Name = "tabGantt";
            tabGantt.Padding = new Padding(4, 5, 4, 5);
            tabGantt.Size = new Size(1392, 1153);
            tabGantt.TabIndex = 1;
            tabGantt.Text = "Gantt Chart";
            tabGantt.UseVisualStyleBackColor = true;
            // 
            // grpZoom
            // 
            grpZoom.Controls.Add(lblZoomValue);
            grpZoom.Controls.Add(trackBarZoom);
            grpZoom.Controls.Add(btnZoomOut);
            grpZoom.Controls.Add(btnZoomIn);
            grpZoom.Controls.Add(btnResetZoom);
            grpZoom.Location = new Point(8, 9);
            grpZoom.Margin = new Padding(4, 5, 4, 5);
            grpZoom.Name = "grpZoom";
            grpZoom.Padding = new Padding(4, 5, 4, 5);
            grpZoom.Size = new Size(1038, 108);
            grpZoom.TabIndex = 1;
            grpZoom.TabStop = false;
            grpZoom.Text = "Zoom Controls";
            // 
            // lblZoomValue
            // 
            lblZoomValue.AutoSize = true;
            lblZoomValue.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblZoomValue.Location = new Point(960, 46);
            lblZoomValue.Margin = new Padding(4, 0, 4, 0);
            lblZoomValue.Name = "lblZoomValue";
            lblZoomValue.Size = new Size(55, 20);
            lblZoomValue.TabIndex = 4;
            lblZoomValue.Text = "100%";
            // 
            // trackBarZoom
            // 
            trackBarZoom.LargeChange = 25;
            trackBarZoom.Location = new Point(467, 28);
            trackBarZoom.Margin = new Padding(4, 5, 4, 5);
            trackBarZoom.Maximum = 300;
            trackBarZoom.Minimum = 25;
            trackBarZoom.Name = "trackBarZoom";
            trackBarZoom.Size = new Size(467, 56);
            trackBarZoom.SmallChange = 10;
            trackBarZoom.TabIndex = 3;
            trackBarZoom.TickFrequency = 25;
            trackBarZoom.Value = 100;
            trackBarZoom.Scroll += trackBarZoom_Scroll;
            // 
            // btnZoomOut
            // 
            btnZoomOut.Location = new Point(160, 39);
            btnZoomOut.Margin = new Padding(4, 5, 4, 5);
            btnZoomOut.Name = "btnZoomOut";
            btnZoomOut.Size = new Size(133, 46);
            btnZoomOut.TabIndex = 2;
            btnZoomOut.Text = "Zoom Out (-)";
            btnZoomOut.UseVisualStyleBackColor = true;
            btnZoomOut.Click += btnZoomOut_Click;
            // 
            // btnZoomIn
            // 
            btnZoomIn.Location = new Point(19, 39);
            btnZoomIn.Margin = new Padding(4, 5, 4, 5);
            btnZoomIn.Name = "btnZoomIn";
            btnZoomIn.Size = new Size(133, 46);
            btnZoomIn.TabIndex = 1;
            btnZoomIn.Text = "Zoom In (+)";
            btnZoomIn.UseVisualStyleBackColor = true;
            btnZoomIn.Click += btnZoomIn_Click;
            // 
            // btnResetZoom
            // 
            btnResetZoom.Location = new Point(301, 39);
            btnResetZoom.Margin = new Padding(4, 5, 4, 5);
            btnResetZoom.Name = "btnResetZoom";
            btnResetZoom.Size = new Size(133, 46);
            btnResetZoom.TabIndex = 0;
            btnResetZoom.Text = "Reset Zoom";
            btnResetZoom.UseVisualStyleBackColor = true;
            btnResetZoom.Click += btnResetZoom_Click;
            // 
            // panelGantt
            // 
            panelGantt.AutoScroll = true;
            panelGantt.BackColor = Color.White;
            panelGantt.BorderStyle = BorderStyle.FixedSingle;
            panelGantt.Location = new Point(8, 126);
            panelGantt.Margin = new Padding(4, 5, 4, 5);
            panelGantt.Name = "panelGantt";
            panelGantt.Size = new Size(1038, 630);
            panelGantt.TabIndex = 0;
            panelGantt.Paint += panelGantt_Paint;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1798, 1055);
            Controls.Add(tabControl);
            Controls.Add(grpControl);
            Controls.Add(grpInput);
            Margin = new Padding(4, 5, 4, 5);
            Name = "Form1";
            Text = "Multilevel Queue Scheduling Simulator";
            grpInput.ResumeLayout(false);
            grpInput.PerformLayout();
            grpControl.ResumeLayout(false);
            grpControl.PerformLayout();
            tabControl.ResumeLayout(false);
            tabQueues.ResumeLayout(false);
            grpCurrentProcess.ResumeLayout(false);
            grpCurrentProcess.PerformLayout();
            grpLog.ResumeLayout(false);
            grpLog.PerformLayout();
            grpQueues.ResumeLayout(false);
            grpQueues.PerformLayout();
            tabGantt.ResumeLayout(false);
            grpZoom.ResumeLayout(false);
            grpZoom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarZoom).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpInput;
        private System.Windows.Forms.Button btnAddRandomProcess;
        private System.Windows.Forms.Button btnAddProcess;
        private System.Windows.Forms.ComboBox cmbQueueType;
        private System.Windows.Forms.TextBox txtBurstTime;
        private System.Windows.Forms.TextBox txtArrivalTime;
        private System.Windows.Forms.TextBox txtProcessName;
        private System.Windows.Forms.Label lblQueueType;
        private System.Windows.Forms.Label lblBurstTime;
        private System.Windows.Forms.Label lblArrivalTime;
        private System.Windows.Forms.Label lblProcessName;
        private System.Windows.Forms.GroupBox grpControl;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnStep;
        private System.Windows.Forms.TextBox txtTimeQuantum;
        private System.Windows.Forms.Label lblTimeQuantum;
        private System.Windows.Forms.Label lblCurrentTime;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabQueues;
        private System.Windows.Forms.GroupBox grpCurrentProcess;
        private System.Windows.Forms.Label lblCurrentProcessInfo;
        private System.Windows.Forms.GroupBox grpLog;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.GroupBox grpQueues;
        private System.Windows.Forms.ListBox lstFCFSQueue;
        private System.Windows.Forms.ListBox lstSRTQueue;
        private System.Windows.Forms.ListBox lstRoundRobinQueue;
        private System.Windows.Forms.ListBox lstWaiting;
        private System.Windows.Forms.Label lblFCFSQueue;
        private System.Windows.Forms.Label lblSRTQueue;
        private System.Windows.Forms.Label lblRoundRobinQueue;
        private System.Windows.Forms.Label lblWaiting;
        private System.Windows.Forms.TabPage tabGantt;
        private System.Windows.Forms.GroupBox grpZoom;
        private System.Windows.Forms.Label lblZoomValue;
        private System.Windows.Forms.TrackBar trackBarZoom;
        private System.Windows.Forms.Button btnZoomOut;
        private System.Windows.Forms.Button btnZoomIn;
        private System.Windows.Forms.Button btnResetZoom;
        private System.Windows.Forms.Panel panelGantt;
    }
}

