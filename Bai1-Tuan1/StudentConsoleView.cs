using System;
using System.Collections.Generic;
using System.Globalization;

namespace StudentManagement
{
    public class StudentConsoleView
    {
        public void DisplayMessage(string message) => Console.WriteLine(message);

        public void DisplayError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[LOI] {message}");
            Console.ResetColor();
        }

        public void DisplayStudents(List<Student> students)
        {
            if (students == null || students.Count == 0)
            {
                Console.WriteLine("Danh sach trong!");
                return;
            }
            foreach (var student in students)
            {
                Console.WriteLine(student.ToString());
            }
        }

        private string ReadLineOrExit()
        {
            string input = Console.ReadLine();
            if (input == null)
            {
                Environment.Exit(0);
            }
            return input;
        }

        public string ReadString(string prompt)
        {
            string input;
            do
            {
                Console.Write(prompt);
                input = ReadLineOrExit();
            } while (string.IsNullOrWhiteSpace(input));
            return input.Trim();
        }

        public double ReadGpa(string prompt)
        {
            double gpa;
            while (true)
            {
                Console.Write(prompt);
                string input = ReadLineOrExit()?.Replace(',', '.') ?? "";
                if (double.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out gpa) && StudentValidator.IsValidGpa(gpa))
                {
                    return gpa;
                }
                DisplayError("Diem phai la so tu 0 den 10.");
            }
        }

        public string ReadEmail(string prompt)
        {
            while (true)
            {
                string email = ReadString(prompt);
                if (StudentValidator.IsValidEmail(email)) return email;
                DisplayError("Email khong dung dinh dang (vi du: abc@domain.com)!");
            }
        }

        public string ReadPhoneNumber(string prompt)
        {
            while (true)
            {
                string phone = ReadString(prompt);
                if (StudentValidator.IsValidPhoneNumber(phone)) return phone;
                DisplayError("So dien thoai khong hop le (phai gom 10 chu so, bat dau bang 0)!");
            }
        }

        public DateTime ReadDateOfBirth(string prompt)
        {
            DateTime dob;
            while (true)
            {
                Console.Write(prompt);
                string input = ReadLineOrExit();
                if (DateTime.TryParseExact(input, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dob)
                    && StudentValidator.IsValidDateOfBirth(dob))
                {
                    return dob;
                }
                DisplayError("Ngay sinh khong hop le! Vui long nhap dung dinh dang dd/MM/yyyy va tuoi tu 15 den 100.");
            }
        }

        public Gender ReadGender(string prompt)
        {
            Gender gender;
            while (true)
            {
                Console.Write(prompt);
                string input = ReadLineOrExit();
                if (int.TryParse(input, out int value) && Enum.IsDefined(typeof(Gender), value))
                {
                    return (Gender)value;
                }
                DisplayError("Gioi tinh khong hop le!");
            }
        }

        public StudyStatus ReadStudyStatus(string prompt)
        {
            StudyStatus status;
            while (true)
            {
                Console.Write(prompt);
                string input = ReadLineOrExit();
                if (int.TryParse(input, out int value) && Enum.IsDefined(typeof(StudyStatus), value))
                {
                    return (StudyStatus)value;
                }
                DisplayError("Trang thai khong hop le!");
            }
        }

        public Student ReadStudentInformation(string currentId = null)
        {
            string id = currentId ?? ReadString("Nhap ma sinh vien: ");
            string name = ReadString("Nhap ho ten: ");
            DateTime dob = ReadDateOfBirth("Nhap ngay sinh (dd/MM/yyyy): ");
            Gender gender = ReadGender("Nhap gioi tinh (0: Male/Nam, 1: Female/Nu, 2: Other/Khac): ");
            string email = ReadEmail("Nhap Email: ");
            string phone = ReadPhoneNumber("Nhap so dien thoai: ");
            string major = ReadString("Nhap nganh hoc: ");
            double gpa = ReadGpa("Nhap diem trung binh: ");
            StudyStatus status = ReadStudyStatus("Nhap trang thai (0: Studying, 1: Graduated, 2: Suspended, 3: DroppedOut): ");

            return new Student(id, name, dob, gender, email, phone, major, gpa, status);
        }
    }
}