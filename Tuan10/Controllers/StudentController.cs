using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tuan10.Models;

namespace Tuan10.Controllers;

public class StudentController : Controller
{
    private static List<Student> listStudents = new List<Student>()
    {
        new Student() { Id = 101, Name = "Hải Nam", Branch = Branch.IT, Gender = Gender.Male, IsRegular = true, Address = "A1-2018", Email = "nam@g.com", DateOfBorth = new DateTime(2003, 1, 15) },
        new Student() { Id = 102, Name = "Minh Tú", Branch = Branch.BE, Gender = Gender.Female, IsRegular = true, Address = "A1-2019", Email = "tu@g.com", DateOfBorth = new DateTime(2003, 5, 20) },
        new Student() { Id = 103, Name = "Hoàng Phong", Branch = Branch.CE, Gender = Gender.Male, IsRegular = false, Address = "A1-2020", Email = "phong@g.com", DateOfBorth = new DateTime(2003, 8, 10) },
        new Student() { Id = 104, Name = "Xuân Mai", Branch = Branch.EE, Gender = Gender.Female, IsRegular = false, Address = "A1-2021", Email = "mai@g.com", DateOfBorth = new DateTime(2003, 11, 25) }
    };

    private readonly IWebHostEnvironment _env;

    public StudentController(IWebHostEnvironment env)
    {
        _env = env;
    }

    [HttpGet]
    [Route("/")]
    [Route("Admin/Student/List")]
    [Route("Student/Index")]
    [Route("Student")]
    public IActionResult Index()
    {
        return View(listStudents);
    }

    [HttpGet]
    [Route("Admin/Student/Add")]
    [Route("Student/Create")]
    public IActionResult Create()
    {
        ViewBag.AllGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();
        ViewBag.AllBranches = new List<SelectListItem>()
        {
            new SelectListItem { Text = "IT", Value = "IT" },
            new SelectListItem { Text = "BE", Value = "BE" },
            new SelectListItem { Text = "CE", Value = "CE" },
            new SelectListItem { Text = "EE", Value = "EE" }
        };
        return View();
    }

    [HttpPost]
    [Route("Admin/Student/Add")]
    [Route("Student/Create")]
    public IActionResult Create(Student s, IFormFile avatarFile)
    {
        if (avatarFile != null && avatarFile.Length > 0)
        {
            var uploadDir = Path.Combine(_env.WebRootPath, "images", "students");
            if (!Directory.Exists(uploadDir))
            {
                Directory.CreateDirectory(uploadDir);
            }
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(avatarFile.FileName);
            var filePath = Path.Combine(uploadDir, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                avatarFile.CopyTo(stream);
            }
            s.Avatar = "/images/students/" + fileName;
        }

        s.Id = listStudents.Any() ? listStudents.Max(x => x.Id) + 1 : 101;
        listStudents.Add(s);
        return View("Index", listStudents);
    }
}
