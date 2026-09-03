
using BusinessLogic.Interface;
using BusinessObject;
using ContosoUniversity.Models;
using DataAccess.EntitySet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Controllers
{
    public class GradesController : Controller
    {
        private readonly IGradeService _gradeService;

        public GradesController(IGradeService gradeService)
        {
            _gradeService = gradeService;
        }

        // GET: /Grades
        public async Task<IActionResult> Index()
        {
            var model = await _gradeService.GetGrades();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Grade/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GradeViewModel model)
        {
            if (ModelState.IsValid)
            {
              model = await _gradeService.CreateGrade(model);
              
               return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }
}

