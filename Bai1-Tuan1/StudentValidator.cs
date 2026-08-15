using System;
using System.Text.RegularExpressions;

namespace StudentManagement
{
    public static class StudentValidator
    {
        public static bool IsValidName(string name)
        {
            return !string.IsNullOrWhiteSpace(name);
        }

        public static bool IsValidGpa(double gpa)
        {
            return gpa >= 0 && gpa <= 10;
        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        public static bool IsValidPhoneNumber(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return false;
            return Regex.IsMatch(phone, @"^0\d{9}$");
        }

        public static bool IsValidDateOfBirth(DateTime dob)
        {
            if (dob >= DateTime.Today) return false;
            int age = DateTime.Today.Year - dob.Year;
            if (dob.Date > DateTime.Today.AddYears(-age)) age--;
            return age >= 15 && age <= 100;
        }
    }
}