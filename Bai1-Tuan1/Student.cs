using System;

namespace StudentManagement
{
    public class Student
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Major { get; set; }
        public double Gpa { get; set; }
        public StudyStatus Status { get; set; }

        public Student()
        {
            Id = string.Empty;
            FullName = string.Empty;
            Email = string.Empty;
            PhoneNumber = string.Empty;
            Major = string.Empty;
        }

        public Student(string id, string fullName, DateTime dob, Gender gender,
                       string email, string phone, string major, double gpa, StudyStatus status)
        {
            Id = id;
            FullName = fullName;
            DateOfBirth = dob;
            Gender = gender;
            Email = email;
            PhoneNumber = phone;
            Major = major;
            Gpa = gpa;
            Status = status;
        }

        public override string ToString()
        {
            return $"[{Id}] {FullName} | {DateOfBirth:dd/MM/yyyy} | {Gender} | {Major} | GPA: {Gpa:F2} | {Status}";
        }
    }
}