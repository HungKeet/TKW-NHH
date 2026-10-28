using System;
using System.Collections.Generic;
using System.Linq;
using MyWebApp.Models;

namespace MyWebApp.Services;

public class StudentRepository
{
    private readonly object sync = new();
    private readonly List<Student> listStudents = new()
    {
        new Student { Id = 101, Name = "Hải Nam", Branch = Branch.IT, Gender = Gender.Male, IsRegular = true, Address = "A1-2018", Email = "nam@gmail.com", DateOfBorth = new DateTime(2003, 1, 15), Score = 8.5 },
        new Student { Id = 102, Name = "Minh Tú", Branch = Branch.BE, Gender = Gender.Female, IsRegular = true, Address = "A1-2019", Email = "tu@gmail.com", DateOfBorth = new DateTime(2003, 5, 20), Score = 7.8 },
        new Student { Id = 103, Name = "Hoàng Phong", Branch = Branch.CE, Gender = Gender.Male, IsRegular = false, Address = "A1-2020", Email = "phong@gmail.com", DateOfBorth = new DateTime(2003, 8, 10), Score = 6.9 },
        new Student { Id = 104, Name = "Xuân Mai", Branch = Branch.EE, Gender = Gender.Female, IsRegular = false, Address = "A1-2021", Email = "mai@gmail.com", DateOfBorth = new DateTime(2003, 11, 25), Score = 9.2 }
    };

    public List<Student> GetAll()
    {
        lock (sync)
            return listStudents.ToList();
    }

    public void Add(Student student)
    {
        lock (sync)
        {
            student.Id = listStudents.Count == 0 ? 101 : listStudents.Max(s => s.Id) + 1;
            student.Password = null;
            listStudents.Add(student);
        }
    }
}
