using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyWebApp.Models;

namespace MyWebApp.Data;

public static class DbInitializer
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new SchoolContext(serviceProvider.GetRequiredService<DbContextOptions<SchoolContext>>());
        context.Database.EnsureCreated();

        if (context.Majors.Any())
        {
            return;
        }

        var majors = new Major[]
        {
            new Major { MajorName = "IT" },
            new Major { MajorName = "Economics" },
            new Major { MajorName = "Mathematics" }
        };

        foreach (var major in majors)
        {
            context.Majors.Add(major);
        }
        context.SaveChanges();

        var learners = new Learner[]
        {
            new Learner { FirstMidName = "Carson", LastName = "Alexander", EnrollmentDate = DateTime.Parse("2005-09-01"), MajorID = 1 },
            new Learner { FirstMidName = "Meredith", LastName = "Alonso", EnrollmentDate = DateTime.Parse("2002-09-01"), MajorID = 2 },
            new Learner { FirstMidName = "Nam", LastName = "Lê", EnrollmentDate = DateTime.Parse("2023-10-04 13:59:00"), MajorID = 3 },
            new Learner { FirstMidName = "Minh", LastName = "Trần", EnrollmentDate = DateTime.Parse("2023-10-04 14:47:00"), MajorID = 2 },
            new Learner { FirstMidName = "Tuấn Nam", LastName = "Trần", EnrollmentDate = DateTime.Parse("2023-10-05 14:46:00"), MajorID = 1 },
            new Learner { FirstMidName = "Bích", LastName = "Vũ", EnrollmentDate = DateTime.Parse("2023-10-23 17:09:00"), MajorID = 2 },
            new Learner { FirstMidName = "Hoàng Nam", LastName = "Nguyễn", EnrollmentDate = DateTime.Parse("2023-10-06 08:30:00"), MajorID = 1 },
            new Learner { FirstMidName = "Phương Thảo", LastName = "Đỗ", EnrollmentDate = DateTime.Parse("2023-10-10 10:15:00"), MajorID = 3 }
        };

        foreach (var l in learners)
        {
            context.Learners.Add(l);
        }
        context.SaveChanges();

        var courses = new Course[]
        {
            new Course { CourseID = 1050, Title = "Chemistry", Credits = 3 },
            new Course { CourseID = 4022, Title = "Microeconomics", Credits = 3 },
            new Course { CourseID = 4041, Title = "Macroeconomics", Credits = 3 }
        };

        foreach (var c in courses)
        {
            context.Courses.Add(c);
        }
        context.SaveChanges();

        var enrollments = new Enrollment[]
        {
            new Enrollment { LearnerID = 1, CourseID = 1050, Grade = 5.5f },
            new Enrollment { LearnerID = 1, CourseID = 4022, Grade = 7.5f },
            new Enrollment { LearnerID = 2, CourseID = 1050, Grade = 3.5f },
            new Enrollment { LearnerID = 2, CourseID = 4041, Grade = 7.0f }
        };

        foreach (var e in enrollments)
        {
            context.Enrollments.Add(e);
        }
        context.SaveChanges();
    }
}
