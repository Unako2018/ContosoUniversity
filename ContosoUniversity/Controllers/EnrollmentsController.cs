using BusinessLogic.Interface;
using BusinessObject;
using BusinessService.Implementation;
using BusinessService.Interface;
using ContosoUniversity.Models;
using DataAccess.EntitySet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
namespace ContosoUniversity.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly IEnrollmentService _enrollmentService;
        private readonly IStudentService _studentService;
        private readonly ICourseService _courseService;
        private readonly IGradeService _gradeService;
        public EnrollmentsController(IEnrollmentService enrollmentService, IStudentService studentService , ICourseService courseService , IGradeService gradeService)
        {
            _enrollmentService = enrollmentService;
            _studentService = studentService;
            _courseService = courseService;
            _gradeService= gradeService;
        }

        // GET: /Enrollments// for index
        public async Task<IActionResult> Index()
        {
            var model = await _enrollmentService.GetEnrollments();
            return View(model);
        }

        // GET: /Enrollments/Create// this is for the create button for enrollments 
        public async Task<IActionResult> Create()
        {
            var students =  await _studentService.GetStudents();
            var courses = await _courseService.GetCourses();
            var grades = await _gradeService.GetGrades();

            ViewBag.Students = new SelectList(students, "ID", "LastName");
            ViewBag.Courses = new SelectList(courses, "CourseID", "Title");

            ViewBag.Grades = new SelectList(grades, "GradeID","Name");
            return View();
        }
        // GET: Enrollments/Create//this is for the edit
        public async Task<IActionResult> Edit(int Id)
        {
            var model = await _enrollmentService.GetEnrollmentById(Id);
            return View(model);

        }

        // GET: Enrollments/Delete/
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _enrollmentService.GetEnrollmentById(id ?? 0);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: ENROLLMENTS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var model = await _enrollmentService.DeleteEnrollment(id ?? 0);
            if (model == false)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));

        }
        // POST: Enrollments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EnrollmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                model = await _enrollmentService.CreateEnrollment(model); // Service saves to DB
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }


        // POST: ENROLLMENTS/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("EnrollmentID , CourseID ,StudentID, Grade")] EnrollmentViewModel model)
        {
            if (id != model.EnrollmentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _enrollmentService.UpdateEnrollment(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

    }
}