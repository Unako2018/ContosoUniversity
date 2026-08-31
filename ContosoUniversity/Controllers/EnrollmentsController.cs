using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ContosoUniversity.Controllers
{
    public class EnrollmentsController : Controller // Defines a controller named EnrollmentsController that inherits from Controller, giving it MVC functionality.
    {
        private readonly SchoolContext _context;//its called a depedency injection .Declares a private field to hold the database context.

        public EnrollmentsController(SchoolContext context)// its called a Constructor injection: ASP.NET Core automatically provides a SchoolContext when this controller is created.
        {
            _context = context;// Stores the injected context in the private field for later use.
        }

        // GET: /Enrollments
        public IActionResult Index()//Handles GET requests to /Enrollments.


        {
            var enrollments = _context.Enrollments//Queries the Enrollments table.


                .Select(e => new // Projects each enrollment into an anonymous object with selected fields
                {
                    e.EnrollmentID,// the enrollment’s ID
                    StudentName = e.Student.LastName,//pulls the student’s last name
                    CourseTitle = e.Course.Title,//pulls the course title
                    e.Grade// the grade 
                })
                .ToList();//Executes the query and returns a list.

            return View(enrollments);//Passes the list to the Razor view for display.
        }

        // GET: /Enrollments/Create
        public IActionResult Create()//Handles GET requests to /Enrollments/Create.
        {
            ViewData["StudentID"] = new SelectList(_context.Students, "ID", "LastName");//Populates a dropdown list of students (ID + LastName).
            ViewData["CourseID"] = new SelectList(_context.Courses, "CourseID", "Title");//Populates a dropdown list of courses (CourseID + Title).
            return View();//Returns the empty form view.
        }

        // POST: /Enrollments/Create
        [HttpPost] //Marks this method to handle POST requests.


        [ValidateAntiForgeryToken] //Protects against CSRF attacks.


        public IActionResult Create(Enrollment enrollment)//Accepts an Enrollment object bound from the form submission.


        {
            if (ModelState.IsValid)//Checks if the submitted data passes validation rules.


            {
                _context.Add(enrollment);//Adds the new enrollment to the DbContext.


                _context.SaveChanges();//Saves changes to the database.


                return RedirectToAction(nameof(Index));//Redirects back to the list of enrollments.


            }
            ViewData["StudentID"] = new SelectList(_context.Students, "ID", "LastName", enrollment.StudentID);//If validation fails, repopulates dropdowns with selected values.


            ViewData["CourseID"] = new SelectList(_context.Courses, "CourseID", "Title", enrollment.CourseID);//If validation fails, repopulates dropdowns with selected values.


            return View(enrollment);//Returns the form again with validation errors highlighted.
        }
    }
}