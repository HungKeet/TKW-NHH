using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentManagement
{
    public class StudentService
    {
        private readonly List<Student> _students;

        public StudentService()
        {
            _students = new List<Student>();
        }

        public bool AddStudent(Student student)
        {
            if (_students.Any(s => s.Id.Equals(student.Id, StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }
            _students.Add(student);
            return true;
        }

        public List<Student> GetAllStudents()
        {
            return _students.ToList();
        }

        public Student FindById(string id)
        {
            return _students.FirstOrDefault(s => s.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
        }

        public List<Student> FindByName(string name)
        {
            return _students.Where(s => s.FullName.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
        }

        public bool UpdateStudent(string id, Student updatedInfo)
        {
            var student = FindById(id);
            if (student == null) return false;

            student.FullName = updatedInfo.FullName;
            student.DateOfBirth = updatedInfo.DateOfBirth;
            student.Gender = updatedInfo.Gender;
            student.Email = updatedInfo.Email;
            student.PhoneNumber = updatedInfo.PhoneNumber;
            student.Major = updatedInfo.Major;
            student.Gpa = updatedInfo.Gpa;
            student.Status = updatedInfo.Status;
            return true;
        }

        public bool DeleteStudent(string id)
        {
            var student = FindById(id);
            if (student == null) return false;

            _students.Remove(student);
            return true;
        }

        public List<Student> SortByName()
        {
            return _students.OrderBy(s => s.FullName, StringComparer.OrdinalIgnoreCase).ToList();
        }

        public List<Student> SortByGpa()
        {
            return _students.OrderByDescending(s => s.Gpa).ToList();
        }

        public List<Student> GetHighAchievers()
        {
            return _students.Where(s => s.Gpa >= 8.0).ToList();
        }

        public List<Student> GetTopStudents()
        {
            if (!_students.Any()) return new List<Student>();
            double maxGpa = _students.Max(s => s.Gpa);
            return _students.Where(s => Math.Abs(s.Gpa - maxGpa) < 0.001).ToList();
        }

        public double CalculateAverageGpa()
        {
            if (!_students.Any()) return 0;
            return _students.Average(s => s.Gpa);
        }

        public Dictionary<string, int> GetStatisticsByMajor()
        {
            return _students
                .GroupBy(s => s.Major.Trim(), StringComparer.OrdinalIgnoreCase)
                .ToDictionary(g => g.Key, g => g.Count());
        }

        public Dictionary<StudyStatus, int> GetStatisticsByStatus()
        {
            return _students.GroupBy(s => s.Status)
                            .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}