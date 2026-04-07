# MLQ_Simulator
Đây là bài tập nhóm môn Hệ Điều Hành về đề tài mô phỏng công cụ lập lịch đa mức để chia các tiến trình vào các hàng đợi khác nhau cho CPU xử lý.
Project Overview:
1) Mục tiêu
Project mô phỏng bộ lập lịch CPU theo mô hình Multilevel Queue (MLQ) với giao diện WinForms, hỗ trợ chạy từng bước và hiển thị Gantt chart.
2) Công nghệ
Ngôn ngữ: C#
Framework: .NET Framework 4.7.2
UI: Windows Forms
Chuẩn code: C# 7.3
3) Kiến trúc MLQ trong project
Hệ thống chia tiến trình thành 3 hàng đợi cố định, ưu tiên từ cao xuống thấp:
QueueType = 1 → Round Robin (RR)
QueueType = 2 → SRT
QueueType = 3 → FCFS
Luật chọn tiến trình luôn theo thứ tự ưu tiên:
Nếu RR có tiến trình ⇒ chạy RR
Nếu RR rỗng, xét SRT
Nếu RR và SRT rỗng, chạy FCFS
4) Đặc điểm lập lịch theo từng queue
Round Robin (ưu tiên cao nhất)
Dùng `TimeQuantum` (mặc định 4 nếu nhập không hợp lệ).
Hết quantum mà chưa xong thì đưa lại cuối hàng RR.
Có thể ngắt queue thấp hơn ngay khi RR có tiến trình mới.
SRT (mức ưu tiên giữa)
Queue được sắp theo `RemainingTime` khi chuẩn bị chọn tiến trình.
Trong chế độ `Step()` của UI: SRT bị ngắt khi có tiến trình RR đến.
Trong `Run()`: có thêm logic ngắt khi xuất hiện tiến trình SRT ngắn hơn.
FCFS (ưu tiên thấp nhất)
Chạy theo thứ tự vào hàng.
Bị ngắt nếu có tiến trình mới vào RR hoặc SRT.
5) Luồng mô phỏng chính
Tiến trình mới được thêm vào danh sách chờ theo `ArrivalTime`.
Mỗi tick thời gian, scheduler nạp tiến trình đã đến vào queue tương ứng.
Chọn tiến trình theo ưu tiên queue.
Chạy 1 đơn vị thời gian (`Step()`), cập nhật `RemainingTime`, `CurrentTime`.
Nếu hoàn thành thì tính các chỉ số hiệu năng.
6) Chỉ số được theo dõi cho mỗi process
`ResponseTime = FirstRunTime - ArrivalTime`
`TurnaroundTime = CompletionTime - ArrivalTime`
`WaitingTime = TurnaroundTime - BurstTime`
`RemainingTime`, `CompletionTime`, trạng thái hoàn thành
7) Gantt chart và UI
Lưu lịch sử chạy bằng `GanttEntry(ProcessName, StartTime, EndTime, QueueType)`.
Màu theo queue:
RR: đỏ
SRT: xanh dương
FCFS: vàng
Có zoom timeline.
Entry `IDLE` được lưu trong lịch sử nhưng không vẽ block process.
8) Ghi chú hành vi hiện tại
`TimeQuantum` bị khóa sau khi thêm process đầu tiên (tránh thay đổi giữa chừng).
Thiết kế ưu tiên cố định có thể gây starvation cho queue thấp khi queue cao liên tục có tiến trình.
Project tập trung vào mô phỏng thuật toán và trực quan hóa, chưa có cơ chế aging.
9) File cốt lõi
`Scheduler.cs`: logic MLQ, preemption, metrics, Gantt history
`Process.cs`: mô hình dữ liệu tiến trình
`GanttEntry.cs`: dữ liệu timeline
`Form1.cs`: UI, nhập process, chạy step, log, vẽ Gantt chart
