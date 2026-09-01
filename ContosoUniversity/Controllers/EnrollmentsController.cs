using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                .Include(e => e.Student)
                .Include(e => e.Course)
                .ToList();

            return View(enrollments);
        }

        // GET: /Enrollments/Create
        public IActionResult Create()
        {
            ViewData["Students"] = new SelectList(_context.Students, "ID", "LastName");
            ViewData["Courses"] = new SelectList(_context.Courses, "CourseID", "Title");
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

            ViewData["Students"] = new SelectList(_context.Students, "ID", "LastName", enrollment.StudentID);
            ViewData["Courses"] = new SelectList(_context.Courses, "CourseID", "Title", enrollment.CourseID);
            return View(enrollment);
        }

        // GET: /Enrollments/Edit/5
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

            ViewData["Students"] = new SelectList(_context.Students, "ID", "LastName", enrollment.StudentID);
            ViewData["Courses"] = new SelectList(_context.Courses, "CourseID", "Title", enrollment.CourseID);
            return View(enrollment);
        }

        // POST: /Enrollments/Edit/5
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

            ViewData["Students"] = new SelectList(_context.Students, "ID", "LastName", enrollment.StudentID);
            ViewData["Courses"] = new SelectList(_context.Courses, "CourseID", "Title", enrollment.CourseID);
            return View(enrollment);
        }

        // GET: /Enrollments/Delete/5
        public IActionResult Delete(int id)
        {
            var enrollment = _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .FirstOrDefault(e => e.EnrollmentID == id);

            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // POST: /Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var enrollment = _context.Enrollments.Find(id);
            if (enrollment != null)
            {
                _context.Enrollments.Remove(enrollment);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}