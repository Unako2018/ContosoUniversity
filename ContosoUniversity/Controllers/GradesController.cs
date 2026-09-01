using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Controllers
{
    public class GradesController : Controller
    {
        private readonly SchoolContext _context;

        public GradesController(SchoolContext context)
        {
            _context = context;
        }

        // GET: /Grades
        public IActionResult Index()
        {
            var enrollments = _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .ToList();

            return View(enrollments);
        }

        // GET: /Grades/Create
        public IActionResult Create()
        {
            ViewData["Students"] = _context.Students.ToList();
            ViewData["Courses"] = _context.Courses.ToList();
            return View();
        }

        // POST: /Grades/Create
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

            // repopulate dropdowns if validation fails
            ViewData["Students"] = _context.Students.ToList();
            ViewData["Courses"] = _context.Courses.ToList();
            return View(enrollment);
        }

        // GET: /Grades/Edit/5
        public IActionResult Edit(int id)
        {
            var enrollment = _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .FirstOrDefault(e => e.EnrollmentID == id);

            if (enrollment == null)
            {
                return NotFound();
            }

            ViewData["Students"] = _context.Students.ToList();
            ViewData["Courses"] = _context.Courses.ToList();
            return View(enrollment);
        }

        // POST: /Grades/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Enrollment enrollment)
        {
            if (id != enrollment.EnrollmentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(enrollment);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Students"] = _context.Students.ToList();
            ViewData["Courses"] = _context.Courses.ToList();
            return View(enrollment);
        }
    }
}
