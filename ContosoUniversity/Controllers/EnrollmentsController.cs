using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ContosoUniversity.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly SchoolContext _context;

        public EnrollmentsController(SchoolContext context)
        {
            _context = context;
        }

        // GET: /Enrollments
        public IActionResult Index()
        {
            var enrollments = _context.Enrollments
                .Select(e => new
                {
                    e.EnrollmentID,
                    StudentName = e.Student.LastName,
                    CourseTitle = e.Course.Title,
                    e.Grade
                })
                .ToList();

            return View(enrollments);
        }

        // GET: /Enrollments/Create
        public IActionResult Create()
        {
            ViewData["StudentID"] = new SelectList(_context.Students, "ID", "LastName");
            ViewData["CourseID"] = new SelectList(_context.Courses, "CourseID", "Title");
            return View();
        }

        // POST: /Enrollments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enrollment);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentID"] = new SelectList(_context.Students, "ID", "LastName", enrollment.StudentID);
            ViewData["CourseID"] = new SelectList(_context.Courses, "CourseID", "Title", enrollment.CourseID);
            return View(enrollment);
        }
    }
}