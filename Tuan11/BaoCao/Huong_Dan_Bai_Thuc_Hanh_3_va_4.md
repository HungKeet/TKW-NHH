# BÁO CÁO VÀ HƯỚNG DẪN CHI TIẾT BÀI THỰC HÀNH 3 VÀ BÀI THỰC HÀNH 4
**Học phần:** Lập Trình Web (ASP.NET Core MVC & Entity Framework Core)

---

# PHẦN I: BÀI THỰC HÀNH 3 – MODEL VALIDATION

## 1. Mục tiêu
Sử dụng các Data Annotation trong ASP.NET Core để kiểm soát dữ liệu người dùng nhập vào (Model Validation) từ phía Server và hỗ trợ Client-side Validation hiển thị thông báo lỗi trực quan bằng tiếng Việt.

## 2. Thư mục dự án
- Vị trí: `D:\LTW-NHH\Tuan10-Lab3\`
- File chính:
  - Model: `Models/Student.cs`
  - Controller: `Controllers/StudentController.cs`
  - View: `Views/Student/Create.cshtml`, `Views/Student/Index.cshtml`

## 3. Chi tiết các Data Annotation áp dụng trên Model Student (`Student.cs`)

| Thuộc tính | Nhãn hiển thị (`[Display]`) | Ràng buộc kiểm soát dữ liệu | Thông báo lỗi tiếng Việt |
| :--- | :--- | :--- | :--- |
| `Id` | Mã sinh viên | Khóa chính, tự tăng | |
| `Name` | Họ và tên | `[Required]`, `[StringLength(100, MinimumLength = 4)]` | "Họ và tên bắt buộc phải nhập", "Họ và tên phải có độ dài từ 4 đến 100 ký tự" |
| `Email` | Địa chỉ Email | `[Required]`, `[RegularExpression(@"^[A-Za-z0-9._%+-]+@gmail\.com$")]` | "Email bắt buộc phải được nhập", "Địa chỉ email phải có đuôi @gmail.com" |
| `Password` | Mật khẩu | `[Required]`, `[DataType(DataType.Password)]`, `[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")]` | "Mật khẩu bắt buộc phải nhập", "Mật khẩu từ 8 ký tự trở lên, bao gồm chữ hoa, chữ thường, chữ số và ký tự đặc biệt (@$!%*?&)" |
| `Branch` | Ngành học | `[Required]` | "Ngành học bắt buộc phải chọn" |
| `Gender` | Giới tính | `[Required]` | "Giới tính bắt buộc phải chọn" |
| `IsRegular` | Hệ đào tạo | `bool` (true: chính quy, false: phi chính quy) | |
| `Address` | Địa chỉ | `[Required]`, `[DataType(DataType.MultilineText)]` | "Địa chỉ bắt buộc phải nhập" |
| `DateOfBorth` | Ngày sinh | `[Required]`, `[DataType(DataType.Date)]`, `[Range(typeof(DateTime), "1/1/1963", "12/31/2005")]` | "Ngày sinh bắt buộc phải nhập", "Ngày sinh phải nằm trong khoảng từ 01/01/1963 đến 31/12/2005" |
| `Score` | Điểm | `[Required]`, `[Range(0.0, 10.0)]` | "Điểm bắt buộc phải nhập", "Điểm phải là số thực từ 0.0 đến 10.0" |

---

# PHẦN II: BÀI THỰC HÀNH 4 – DATA ACCESS BẰNG ENTITY FRAMEWORK CORE

## 1. Cấu trúc Solution và Dự án
- **Solution:** `D:\LTW-NHH\MyWebApp\MyWebApp.sln`
  - **Dự án 1 (Code First):** `D:\LTW-NHH\MyWebApp\MyWebApp\`
  - **Dự án 2 (Database First):** `D:\LTW-NHH\MyWebApp\DBFirstApp\`
- **Database:** Microsoft SQL Server `School` trên `(local)\SQLEXPRESS`.

---

## 2. Phần I: Cách tiếp cận Code First (`MyWebApp`)

### 2.1. Quy trình thực hiện
1. **Định nghĩa các thực thể Model C#**:
   - `Major`: `MajorID`, `MajorName`, navigation property `Learners`.
   - `Learner`: `LearnerID`, `LastName`, `FirstMidName`, `EnrollmentDate`, `MajorID`, foreign key `Major`, navigation property `Enrollments`.
   - `Course`: `CourseID` (`[DatabaseGenerated(DatabaseGeneratedOption.None)]`), `Title`, `Credits`, navigation property `Enrollments`.
   - `Enrollment`: `EnrollmentID`, `CourseID`, `LearnerID`, `Grade` (`float`), foreign keys `Learner`, `Course`.

2. **Xây dựng lớp `SchoolContext`**:
   - Kế thừa từ `DbContext`.
   - Khai báo các `DbSet<Major>`, `DbSet<Learner>`, `DbSet<Course>`, `DbSet<Enrollment>`.
   - Cấu hình tên bảng trong `OnModelCreating`: ánh xạ bảng số ít (`Major`, `Course`, `Learner`, `Enrollment`).

3. **Cấu hình Connection String (`appsettings.json`)**:
   ```json
   "ConnectionStrings": {
     "SchoolContext": "Server=.\\SQLEXPRESS;Database=School;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
   }
   ```

4. **Khởi tạo và Seed Data tự động (`DbInitializer`)**:
   - Gọi `context.Database.EnsureCreated()` để tự động sinh CSDL `School` và các bảng trên SQL Server nếu chưa tồn tại.
   - Nạp dữ liệu mẫu ban đầu:
     - 3 Ngành học: `IT`, `Economics`, `Mathematics`.
     - 2 Học viên: `Carson Alexander`, `Meredith Alonso`.
     - 3 Khóa học: `Chemistry` (1050), `Microeconomics` (4022), `Macroeconomics` (4041).
     - 4 Đăng ký học (Enrollments) với điểm số `Grade` tương ứng.

5. **Xây dựng CRUD trên `LearnerController`**:
   - **Index (List)**: Lấy danh sách học viên kèm theo tên chuyên ngành (`.Include(m => m.Major)`).
   - **Create**:
     - GET: Tạo `SelectList` chuyên ngành gửi sang View qua `ViewBag.MajorID`.
     - POST: Nhận đối tượng `Learner`, lưu vào cơ sở dữ liệu `School` và chuyển về `Index`.
   - **Edit**:
     - GET: Lấy thông tin học viên theo ID, bind `MajorID` tương ứng.
     - POST: Cập nhật đối tượng `Learner`, xử lý `DbUpdateConcurrencyException`.
   - **Delete**:
     - GET: Kiểm tra nếu học viên đã có bản ghi đăng ký khóa học (`learner.Enrollments.Count > 0`) thì thông báo:
       `"This learner has some enrollments, can't delete!"`.
     - POST: Xóa học viên khỏi bảng `Learner` khi không còn ràng buộc.

---

## 3. Phần II: Cách tiếp cận Database First (`DBFirstApp`)

### 3.1. Quy trình thực hiện
1. **Tạo dự án mới `DBFirstApp`** trong cùng Solution.
2. **Cài đặt các gói EF Core**:
   - `Microsoft.EntityFrameworkCore.SqlServer`
   - `Microsoft.EntityFrameworkCore.Design`
   - `Microsoft.EntityFrameworkCore.Tools`
3. **Chạy lệnh Scaffolding (Reverse Engineering)** từ Database `School` có sẵn:
   ```powershell
   dotnet ef dbcontext scaffold "Server=.\SQLEXPRESS;Database=School;Trusted_Connection=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer --output-dir Models --context SchoolContext
   ```
4. **Kết quả Scaffolding sinh tự động**:
   - Các class Model: `Course.cs`, `Enrollment.cs`, `Learner.cs`, `Major.cs`.
   - Lớp `SchoolContext.cs` với cấu hình Fluent API đầy đủ các khóa ngoại, quan hệ 1-N.
5. **Xây dựng Scaffolding Controller**:
   - `LearnersController.cs` kèm theo đầy đủ các View `Index`, `Create`, `Edit`, `Details`, `Delete`.

---

## 4. So sánh hai cách tiếp cận: Code First vs Database First

| Tiêu chí | Code First | Database First |
| :--- | :--- | :--- |
| **Điểm bắt đầu** | Viết các lớp Model trong mã nguồn C# trước. | Thiết kế lược đồ CSDL trên SQL Server (hoặc hệ quản trị CSDL) trước. |
| **Cơ chế sinh CSDL/Code** | EF Core đọc Model C# và sinh ra Database (`EnsureCreated` hoặc `Migrations`). | Công cụ Scaffolding (`Scaffold-DbContext`) đọc CSDL và sinh tự động Model C# & DbContext. |
| **Ưu điểm** | - Lập trình viên làm chủ 100% mã nguồn đối tượng.<br>- Quản lý thay đổi cấu trúc CSDL bằng mã nguồn (Code Migrations).<br>- Tối ưu cho dự án mới hoàn toàn (Greenfield). | - Tận dụng tối đa CSDL đã có sẵn (Legacy/Brownfield).<br>- Thuận lợi cho đội ngũ DBA chuyên môn thiết kế và tối ưu chỉ mục, khóa, trigger trên CSDL. |
| **Nhược điểm** | - Đòi hỏi hiểu sâu về Fluent API và Data Annotations để cấu hình ràng buộc phức tạp. | - Nếu CSDL thay đổi thì phải re-scaffold hoặc chỉnh sửa cẩn thận, tránh ghi đè mã nguồn bổ sung. |
| **Đối tượng phù hợp** | Các ứng dụng Web hiện đại, quy trình Agile, CI/CD. | Doanh nghiệp lớn có CSDL tập trung dùng chung giữa nhiều hệ thống. |
