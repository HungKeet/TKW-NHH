using System;
using System.IO;

namespace StudentManagement
{
    public class MenuManager
    {
        private readonly StudentService _service;
        private readonly StudentConsoleView _view;

        public MenuManager()
        {
            _service = new StudentService();
            _view = new StudentConsoleView();
        }

        public void Run()
        {
            while (true)
            {
                SafeClear();
                Console.WriteLine("=== CHUONG TRINH QUAN LY SINH VIEN ===");
                Console.WriteLine("1. Them sinh vien");
                Console.WriteLine("2. Hien thi danh sach");
                Console.WriteLine("3. Tim sinh vien theo ma");
                Console.WriteLine("4. Tim gan dung theo ho ten");
                Console.WriteLine("5. Cap nhat sinh vien");
                Console.WriteLine("6. Xoa sinh vien");
                Console.WriteLine("7. Sap xep theo ho ten");
                Console.WriteLine("8. Sap xep theo diem trung binh");
                Console.WriteLine("9. Sinh vien co diem tu 8 tro len");
                Console.WriteLine("10. Sinh vien co diem cao nhat");
                Console.WriteLine("11. Tinh diem trung binh toan bo");
                Console.WriteLine("12. Thong ke theo nganh");
                Console.WriteLine("13. Thong ke theo trang thai");
                Console.WriteLine("0. Thoat");
                Console.Write("Chon chuc nang: ");

                string choice = Console.ReadLine();
                if (choice == null) break;

                ProcessChoice(choice);

                if (choice == "0") break;

                Console.WriteLine("\nNhan phim bat ky de tiep tuc...");
                Console.ReadKey();
            }
        }

        private static void SafeClear()
        {
            try
            {
                Console.Clear();
            }
            catch (IOException)
            {
            }
        }

        private void ProcessChoice(string choice)
        {
            Console.WriteLine("--------------------------------------");
            switch (choice)
            {
                case "1":
                    var newStudent = _view.ReadStudentInformation();
                    if (_service.AddStudent(newStudent)) _view.DisplayMessage("Them thanh cong!");
                    else _view.DisplayError("Ma sinh vien da ton tai!");
                    break;
                case "2":
                    _view.DisplayStudents(_service.GetAllStudents());
                    break;
                case "3":
                    string idToFind = _view.ReadString("Nhap ma sinh vien can tim: ");
                    var student = _service.FindById(idToFind);
                    if (student != null) _view.DisplayMessage(student.ToString());
                    else _view.DisplayError("Khong tim thay sinh vien!");
                    break;
                case "4":
                    string nameToFind = _view.ReadString("Nhap ten can tim: ");
                    _view.DisplayStudents(_service.FindByName(nameToFind));
                    break;
                case "5":
                    string idToUpdate = _view.ReadString("Nhap ma sinh vien can cap nhat: ");
                    if (_service.FindById(idToUpdate) == null)
                    {
                        _view.DisplayError("Sinh vien khong ton tai!");
                        break;
                    }
                    var updatedInfo = _view.ReadStudentInformation(idToUpdate);
                    if (_service.UpdateStudent(idToUpdate, updatedInfo)) _view.DisplayMessage("Cap nhat thanh cong!");
                    break;
                case "6":
                    string idToDelete = _view.ReadString("Nhap ma sinh vien can xoa: ");
                    if (_service.DeleteStudent(idToDelete)) _view.DisplayMessage("Xoa thanh cong!");
                    else _view.DisplayError("Sinh vien khong ton tai!");
                    break;
                case "7":
                    _view.DisplayStudents(_service.SortByName());
                    break;
                case "8":
                    _view.DisplayStudents(_service.SortByGpa());
                    break;
                case "9":
                    _view.DisplayStudents(_service.GetHighAchievers());
                    break;
                case "10":
                    _view.DisplayStudents(_service.GetTopStudents());
                    break;
                case "11":
                    _view.DisplayMessage($"Diem trung binh toan bo: {_service.CalculateAverageGpa():F2}");
                    break;
                case "12":
                    foreach (var stat in _service.GetStatisticsByMajor())
                        _view.DisplayMessage($"- Nganh {stat.Key}: {stat.Value} sinh vien");
                    break;
                case "13":
                    foreach (var stat in _service.GetStatisticsByStatus())
                        _view.DisplayMessage($"- Trang thai {stat.Key}: {stat.Value} sinh vien");
                    break;
                case "0":
                    _view.DisplayMessage("Tam biet!");
                    break;
                default:
                    _view.DisplayError("Chuc nang khong hop le!");
                    break;
            }
        }
    }
}