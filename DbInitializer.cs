
using DataAccess.EntitySet;
using System;
using System.Linq;

namespace ContonsoUniversity.Data

{
    public static class DbInitializer
    {
        public static void Initialize(SchoolContext context)
        {
            context.Database.EnsureCreated();

            // If any students exist, assume DB has been seeded
            if (context.Students.Any())
            {
                return;
            }

            //  Seed Students
            var students = new Student[]
            {
                new Student{FirstMidName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2005-09-01")},
                new Student{FirstMidName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2002-09-01")},
                new Student{FirstMidName="Arturo",LastName="Anand",EnrollmentDate=DateTime.Parse("2003-09-01")},
                new Student{FirstMidName="Gytis",LastName="Barzdukas",EnrollmentDate=DateTime.Parse("2002-09-01")},
                new Student{FirstMidName="Yan",LastName="Li",EnrollmentDate=DateTime.Parse("2002-09-01")},
                new Student{FirstMidName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2001-09-01")},
                new Student{FirstMidName="Laura",LastName="Norman",EnrollmentDate=DateTime.Parse("2003-09-01")},
                new Student{FirstMidName="Nino",LastName="Olivetto",EnrollmentDate=DateTime.Parse("2005-09-01")}
            };
            context.Students.AddRange(students);
            context.SaveChanges();

            // - Seed Courses 
            var courses = new Course[]
            {
                new Course{CourseID=1050,Title="Chemistry",Credits=3},
                new Course{CourseID=4022,Title="Microeconomics",Credits=3},
                new Course{CourseID=4041,Title="Macroeconomics",Credits=3},
                new Course{CourseID=1045,Title="Calculus",Credits=4},
                new Course{CourseID=3141,Title="Trigonometry",Credits=4},
                new Course{CourseID=2021,Title="Composition",Credits=3},
                new Course{CourseID=2042,Title="Literature",Credits=4}
            };
            context.Courses.AddRange(courses);
            context.SaveChanges();

            //  Seed Grades 
            var grades = new Grade[]
            {
                new Grade{GradeID=1, Name="A", Description="Excellent"},
                new Grade{GradeID=2, Name="B", Description="Good"},
                new Grade{GradeID=3, Name="C", Description="Average"},
                new Grade{GradeID=4, Name="D", Description="Poor"},
                new Grade{GradeID=5, Name="F", Description="Fail"}
            };
            context.Grades.AddRange(grades);
            context.SaveChanges();

            //  Seed Enrollments 
            var enrollments = new Enrollment[]
            {
                new Enrollment{StudentID=1,CourseID=1050,GradeID=1}, // A
                new Enrollment{StudentID=1,CourseID=4022,GradeID=3}, // C
                new Enrollment{StudentID=1,CourseID=4041,GradeID=2}, // B
                new Enrollment{StudentID=2,CourseID=1045,GradeID=2}, // B
                new Enrollment{StudentID=2,CourseID=3141,GradeID=5}, // F
                new Enrollment{StudentID=2,CourseID=2021,GradeID=5}, // F
                new Enrollment{StudentID=3,CourseID=1050,GradeID=2}, // B
                new Enrollment{StudentID=4,CourseID=1050,GradeID=3}, // C
                new Enrollment{StudentID=4,CourseID=4022,GradeID=5}, // F
                new Enrollment{StudentID=5,CourseID=4041,GradeID=3}, // C
                new Enrollment{StudentID=6,CourseID=1045,GradeID=2}, // B
                new Enrollment{StudentID=7,CourseID=3141,GradeID=1}, // A
            };
            context.Enrollments.AddRange(enrollments);
            context.SaveChanges();
        }
    }