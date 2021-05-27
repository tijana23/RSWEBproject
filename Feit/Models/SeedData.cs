using Feit.Areas.Identity.Data;
using Feit.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feit.Models
{
    public class SeedData
    {
        public static async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<FeitUser>>();
            IdentityResult roleResult;
            //Add Admin Role
            var roleCheck = await RoleManager.RoleExistsAsync("Admin");
            if (!roleCheck) { roleResult = await RoleManager.CreateAsync(new IdentityRole("Admin")); }
           
            FeitUser user = await UserManager.FindByEmailAsync("admin@feit.com");
            if (user == null)
            {
                var User = new FeitUser();
                User.Email = "admin@feit.com";
                User.UserName = "admin@feit.com";
                string userPWD = "Admin123";
                IdentityResult chkUser = await UserManager.CreateAsync(User, userPWD);
                //Add default User to Role Admin
                if (chkUser.Succeeded) { var result1 = await UserManager.AddToRoleAsync(User, "Admin"); }
            }
        }
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new FeitContext(serviceProvider.GetRequiredService<DbContextOptions<FeitContext>>()))
            {
                CreateUserRoles(serviceProvider).Wait();
                if (context.Student.Any() || context.Teacher.Any() || context.Course.Any())
                {
                    return;
                }
                context.Teacher.AddRange(
                    new Teacher
                    {
                        FirstName = "Даниел",
                        LastName = "Денковски",
                        Degree = "Доктор на науки",
                        AcademicRank = "Доцент",
                        OfficeNumber = "121б",
                        HireDate = DateTime.Parse("2017-1-01")
                    },

                    new Teacher
                    {
                        FirstName = "Перо",
                        LastName = "Латкоски",
                        Degree = "Доктор на науки",
                        AcademicRank = "Ред. Професор",
                        OfficeNumber = "ТК",
                        HireDate = DateTime.Parse("2006-1-01")
                    },

                    new Teacher
                    {
                        FirstName = "Марија",
                        LastName = "Календар",
                        Degree = "Доктор на науки",
                        AcademicRank = "Ред. Професор",
                        OfficeNumber = "ФЛАОП",
                        HireDate = DateTime.Parse("2006-1-01")
                    },

                    new Teacher
                    {
                        FirstName = "Ана",
                        LastName = "Чолакоска",
                        Degree = "Магистер",
                        AcademicRank = "Асистент",
                        OfficeNumber = "121б",
                        HireDate = DateTime.Parse("2017-1-01")
                    }
                );
                context.SaveChanges();

                context.Student.AddRange(
                    new Student
                    {
                        StudentId = "15/2017",
                        FirstName = "Симона",
                        LastName = "Малевска",
                        EnrollmentDate = DateTime.Parse("2017-9-01"),
                        AcquiredCredits = 153,
                        CurrentSemestar = 8,
                        EducationLevel = "Додипломец",
                    },

                    new Student
                    {
                        StudentId = "257/2017",
                        FirstName = "Тијана",
                        LastName = "Настевска",
                        EnrollmentDate = DateTime.Parse("2017-9-01"),
                        AcquiredCredits = 147,
                        CurrentSemestar = 8,
                        EducationLevel = "Додипломец",
                    }
                );
                context.SaveChanges();

                context.Course.AddRange(
                    new Course
                    {
                        Title = "Развој на серверски WEB апликации",
                        Credits = 6,
                        Semester = 6,
                        Programme = "КТИ, ТКИИ",
                        EducationLevel = "Додипломски",
                        FirstTeacherId = context.Teacher.Single(t => t.FirstName == "Даниел" && t.LastName == "Денковски").Id,
                        SecondTeacherId = context.Teacher.Single(t => t.FirstName == "Перо" && t.LastName == "Латкоски").Id
                    },

                    new Course
                    {
                        Title = "Микропроцесорски системи",
                        Credits = 6,
                        Semester = 6,
                        Programme = "КТИ",
                        EducationLevel = "Додипломски",
                        FirstTeacherId = context.Teacher.Single(t => t.FirstName == "Марија" && t.LastName == "Календар").Id,
                        SecondTeacherId = context.Teacher.Single(t => t.FirstName == "Ана" && t.LastName == "Чолакоска").Id
                    }
                );
                context.SaveChanges();

                context.Enrollment.AddRange(
                    new Enrollment
                    {
                        CourseId = context.Course.Single(t => t.Title == "Развој на серверски WEB апликации").Id,
                        StudentId = context.Student.Single(t => t.FirstName == "Симона" && t.LastName == "Малевска").Id,
                        Semester = "летен",
                        Year = 2021,
                        Grade = 0,
                        SeminalUrl = " ",
                        ProjectUrl = " ",
                        ExamPoints = 0,
                        SeminalPoints = 0,
                        ProjectPoints = 0,
                        AdditionalPoints = 0,
                        FinishDate = DateTime.Parse("2000-1-01"),
                    },

                    new Enrollment
                    {
                        CourseId = context.Course.Single(t => t.Title == "Развој на серверски WEB апликации").Id,
                        StudentId = context.Student.Single(t => t.FirstName == "Тијана" && t.LastName == "Настевска").Id,
                        Semester = "летен",
                        Year = 2021,
                        Grade = 0,
                        SeminalUrl = " ",
                        ProjectUrl = " ",
                        ExamPoints = 0,
                        SeminalPoints = 0,
                        ProjectPoints = 0,
                        AdditionalPoints = 0,
                        FinishDate = DateTime.Parse("2000-1-01"),
                    },

                    new Enrollment
                    {
                        CourseId = context.Course.Single(t => t.Title == "Микропроцесорски системи").Id,
                        StudentId = context.Student.Single(t => t.FirstName == "Симона" && t.LastName == "Малевска").Id,
                        Semester = "летен",
                        Year = 2020,
                        Grade = 7,
                        SeminalUrl = " ",
                        ProjectUrl = " ",
                        ExamPoints = 67,
                        SeminalPoints = 0,
                        ProjectPoints = 0,
                        AdditionalPoints = 0,
                        FinishDate = DateTime.Parse("2021-1-23"),
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
