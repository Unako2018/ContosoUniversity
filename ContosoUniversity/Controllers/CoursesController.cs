using BusinessService.Implementation;
using BusinessLogic.Interface;
using BusinessObject;
using ContosoUniversity.Models;
using DataAccess.EntitySet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace ContosoUniversity.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        // GET: /Courses// for index
        public async Task<IActionResult> Index()
        {
            var model = await _courseService.GetCourses();
            return View(model);
        }

        // GET: /Courses/Create// this is for the create button for Courses 
        public IActionResult Create()
        {
            return View();
        }

        // GET: Courses /Create//this is for the edit
        public async Task<IActionResult> Edit(int Id)
        {
            var model = await _courseService.GetCourseById(Id);
            return View(model);

        }


        // GET: COURSES/Delete/
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _courseService.GetCourseById(id ?? 0);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }


        // POST: COURSES/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var model = await _courseService.DeleteCourse(id ?? 0);
            if (model == false)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));

        }

        // POST: Courses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseViewModel model)
        {
            if (ModelState.IsValid)
            {
                model = await _courseService.CreateCourse(model); // Service saves to DB
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // POST: COURSES/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("CourseID,Title,Credits ")] CourseViewModel model)
        {
            if (id != model.CourseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _courseService.UpdateCourse(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }
}




        