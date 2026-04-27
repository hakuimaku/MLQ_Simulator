# MLQ_Simulator

Ứng dụng mô phỏng **Multilevel Queue Scheduling** được xây dựng bằng **C# WinForms** trên **.NET 10**.

- Đây là bài tập nhóm học phần Hệ điều hành
- Giảng viên hướng dẫn: Hồ Hải Quân
- Lớp học phần: 15DHBM04
- Đề tài: Phát triển công cụ lập lịch đa mức (Multilevel Queue Scheduling)

## NHẬT KÝ LÀM VIỆC CỦA NHÓM
| MSSV | Họ Tên | Công việc | % Hoàn thành
|------|------------|-----------|--------|
| 2001240499 | Võ Châu Bảo Tiến (nhóm trưởng) | Xây dựng nền tảng chương trình trên console, tạo cấu trúc chương trình. | 100% |
| 2001240045 | Huỳnh Thanh Bình | Hoàn thiện chương trình trên winform, vẽ biểu đồ gantt minh họa. (Ghi chú: Hoàn thành công việc không đúng thời hạn được phân công) | 80% |
| 2001240553 | Đỗ Ngọc Việt | Viết tiểu luận Word, làm bản trình bày trên PowerPoint lý thuyết của đề tài. | 100% |



## Mục tiêu

Project này mô phỏng cách hệ điều hành phân phối CPU cho nhiều nhóm tiến trình theo mô hình **đa hàng đợi ưu tiên**:

- **Queue 1**: Round Robin
- **Queue 2**: SRT (Shortest Remaining Time)
- **Queue 3**: FCFS (First Come First Served)

Ứng dụng cho phép:

- thêm tiến trình thủ công hoặc ngẫu nhiên,
- chạy mô phỏng theo từng bước,
- quan sát trạng thái các hàng đợi,
- xem log thực thi,
- vẽ Gantt chart cho lịch sử chạy CPU.

---

## Công nghệ sử dụng

- **C# 14.0**
- **.NET 10**
- **Windows Forms**
- `System.Drawing` cho Gantt chart
- `System.Windows.Forms` cho giao diện mô phỏng

---

## Cấu trúc project

Các file chính:

- `Program.cs`  
  Điểm khởi động ứng dụng.

- `Form1.cs`  
  Chứa giao diện chính, xử lý sự kiện, hiển thị queue, log, Gantt chart.

- `Scheduler.cs`  
  Chứa logic mô phỏng lập lịch CPU.

- `Process.cs`  
  Mô hình dữ liệu của một tiến trình.

- `GanttEntry.cs`  
  Mỗi đoạn thực thi trên timeline Gantt.

---

## Mô hình dữ liệu

### `Process`

Một tiến trình bao gồm:

- `PID`: mã định danh
- `Name`: tên hiển thị
- `ArrivalTime`: thời điểm đến
- `BurstTime`: thời gian xử lý ban đầu
- `QueueType`: loại hàng đợi
- `RemainingTime`: thời gian còn lại
- `CompletionTime`: thời điểm hoàn thành
- `WaitingTime`: thời gian chờ
- `TurnaroundTime`: thời gian lưu lại hệ thống
- `ResponseTime`: thời gian phản hồi
- `FirstRunTime`: thời điểm chạy lần đầu
- `IsCompleted`: trạng thái hoàn thành

### `GanttEntry`

Mỗi entry trên Gantt chart gồm:

- tên tiến trình,
- thời gian bắt đầu,
- thời gian kết thúc,
- loại queue.

---

## Nguyên lý hoạt động của mô phỏng

Ứng dụng chạy theo cơ chế **step-by-step**.  
Mỗi lần bấm **Next Step** sẽ mô phỏng **1 đơn vị thời gian**.

### 1. Nạp tiến trình vào hàng đợi

Tại mỗi bước, scheduler kiểm tra các tiến trình trong danh sách chờ:

- nếu `ArrivalTime <= CurrentTime`, tiến trình được chuyển vào queue tương ứng;
- tiến trình sau khi vào queue sẽ bị xóa khỏi danh sách chờ.

### 2. Chọn tiến trình để chạy

Ưu tiên chọn CPU theo thứ tự:

1. **Round Robin Queue**
2. **SRT Queue**
3. **FCFS Queue**

Nghĩa là nếu có tiến trình ở queue ưu tiên cao hơn, nó sẽ được chọn trước.

### 3. Thực thi 1 đơn vị thời gian

Tiến trình đang chạy sẽ:

- giảm `RemainingTime` đi 1,
- tăng `CurrentTime` lên 1,
- kiểm tra các tiến trình mới đến trong thời điểm đó.

### 4. Kiểm tra hoàn thành

Nếu `RemainingTime == 0`:

- tiến trình được đánh dấu hoàn thành,
- tính:
  - `CompletionTime`
  - `TurnaroundTime = CompletionTime - ArrivalTime`
  - `WaitingTime = TurnaroundTime - BurstTime`
  - `ResponseTime = FirstRunTime - ArrivalTime`

### 5. Kiểm tra ngắt CPU

Sau mỗi tick, scheduler kiểm tra các điều kiện preempt:

- **Round Robin**: bị ngắt khi hết `TimeQuantum`
- **SRT**: bị ngắt nếu có tiến trình Round Robin đến
- **FCFS**: bị ngắt nếu có tiến trình Round Robin hoặc SRT đến

---

## Chính sách của từng queue

| Queue | Thuật toán | Cách chạy |
|------|------------|-----------|
| 1 | Round Robin | Chạy theo quantum |
| 2 | SRT | Chọn tiến trình có `RemainingTime` nhỏ nhất |
| 3 | FCFS | Chạy theo thứ tự đến |

### Lưu ý về độ ưu tiên

Đây là mô hình **strict priority** giữa các hàng đợi:

- Queue 1 luôn cao nhất
- Queue 2 đứng sau
- Queue 3 thấp nhất

---

## Giao diện ứng dụng

### Tab `Queues & Logs`

Hiển thị:

- danh sách tiến trình đang chờ,
- Round Robin Queue,
- SRT Queue,
- FCFS Queue,
- tiến trình đang chạy,
- log thực thi.

### Tab `Gantt Chart`

Hiển thị:

- timeline chạy CPU,
- màu khác nhau theo queue,
- phóng to/thu nhỏ bằng zoom.

---

## Hướng dẫn sử dụng

### 1. Khởi chạy ứng dụng

Mở solution bằng **Visual Studio Community 2026** và chạy project.

### 2. Thiết lập Time Quantum

- nhập giá trị vào ô **Time Quantum**,
- giá trị phải lớn hơn 0.

### 3. Thêm tiến trình

Có 2 cách:

#### Thêm thủ công
Nhập:

- `Process Name`
- `Arrival Time`
- `Burst Time`
- chọn `Queue Type`

Sau đó bấm **Add Process**.

#### Thêm ngẫu nhiên
Bấm **Add Random Process**.

Hệ thống sẽ tự tạo:

- tên tiến trình theo queue,
- arrival time ngẫu nhiên,
- burst time ngẫu nhiên.

### 4. Chạy mô phỏng

Bấm **Next Step** để mô phỏng từng bước.

### 5. Xem kết quả

Quan sát:

- danh sách queue,
- log thực thi,
- trạng thái CPU,
- Gantt chart.

### 6. Reset mô phỏng

Bấm **Reset** để:

- xóa dữ liệu hiện tại,
- bật lại Time Quantum,
- bắt đầu mô phỏng mới.

---

## Ý nghĩa màu sắc trên Gantt chart

Màu tiến trình được gán theo queue:

- **Round Robin**: đỏ
- **SRT**: xanh dương
- **FCFS**: vàng

---

## Lưu ý quan trọng

### 1. Đây là mô phỏng step-by-step
Ứng dụng trong giao diện hiện tại chạy bằng cách bấm từng bước, không phải chạy full tự động từ đầu đến cuối.

### 2. Time Quantum chỉ áp dụng cho Round Robin
`Time Quantum` không ảnh hưởng đến SRT hoặc FCFS.

### 3. Time Quantum bị khóa sau khi đã thêm tiến trình
Khi đã thêm tiến trình đầu tiên, ô nhập Time Quantum sẽ bị vô hiệu hóa để tránh thay đổi giữa chừng.  
Muốn đổi lại, hãy bấm **Reset**.

### 4. SRT trong project là dạng preemptive
Queue SRT được sắp xếp theo `RemainingTime` tăng dần.  
Tiến trình đang chạy có thể bị ngắt nếu có:

- tiến trình Round Robin đến,
- hoặc tiến trình SRT khác có thời gian còn lại nhỏ hơn.

### 5. Gantt chart không hiển thị block `IDLE`
Khoảng CPU rảnh có thể được ghi trong lịch sử nội bộ, nhưng phần vẽ Gantt chart chỉ tập trung vào tiến trình đang chạy.

### 6. Input phải hợp lệ
Các trường:

- `Arrival Time`
- `Burst Time`
- `Time Quantum`

đều cần là số nguyên hợp lệ.

### 7. Không hỗ trợ sửa/xóa tiến trình
Phiên bản hiện tại chỉ hỗ trợ thêm tiến trình và reset toàn bộ mô phỏng.

### 8. Dữ liệu random mang tính minh họa
Tiến trình ngẫu nhiên được tạo với:

- `ArrivalTime` = thời gian hiện tại + 1 đến + 10
- `BurstTime` = 4 đến 20

---

## Hạn chế của mô phỏng

- Chưa có chức năng lưu/đọc dữ liệu từ file.
- Chưa hỗ trợ chỉnh sửa tiến trình đã thêm.
- Chưa hỗ trợ chạy tự động hoàn toàn theo một nút duy nhất.
- Chưa có thống kê tổng hợp cuối mô phỏng theo bảng.
- Gantt chart chỉ phục vụ trực quan hóa, không phải trình mô phỏng thời gian thực của hệ điều hành.

---

## Mô tả logic trong `Scheduler`

`Scheduler` giữ các thành phần chính:

- `ArrivalProcessList`: danh sách tiến trình chưa đến
- `RoundRobinQueue`: hàng đợi Round Robin
- `SRTQueue`: hàng đợi SRT
- `FCFSQueue`: hàng đợi FCFS
- `CurrentProcess`: tiến trình đang chạy
- `CurrentTime`: thời gian mô phỏng hiện tại
- `GanttHistory`: lịch sử thực thi dùng để vẽ biểu đồ

### Quy trình của `Step()`

1. Nạp tiến trình đến đúng thời điểm.
2. Nếu CPU đang rảnh, chọn tiến trình theo ưu tiên queue.
3. Chạy 1 tick.
4. Kiểm tra hoàn thành hoặc bị ngắt.
5. Cập nhật log và Gantt history.

---

## Chạy project bằng Visual Studio

1. Mở `MLQ_Simulator.sln`
2. Chọn cấu hình build phù hợp
3. Nhấn **Start** hoặc `F5`
4. Thực hiện mô phỏng trên giao diện

---

## Ghi chú cuối

Project này phù hợp để học và minh họa:

- scheduling đa hàng đợi,
- Round Robin,
- SRT,
- FCFS,
- preemption,
- trực quan hóa Gantt chart.

Nếu muốn, có thể mở rộng thêm:

- tự động chạy đến khi kết thúc,
- xuất kết quả ra CSV/Excel,
- lưu và tải danh sách tiến trình,
- hiển thị thống kê trung bình:
  - waiting time,
  - turnaround time,
  - response time.Project tập trung vào mô phỏng thuật toán và trực quan hóa, chưa có cơ chế aging.
9) File cốt lõi
`Scheduler.cs`: logic MLQ, preemption, metrics, Gantt history
`Process.cs`: mô hình dữ liệu tiến trình
`GanttEntry.cs`: dữ liệu timeline
`Form1.cs`: UI, nhập process, chạy step, log, vẽ Gantt chart
