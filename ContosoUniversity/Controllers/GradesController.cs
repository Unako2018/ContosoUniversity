using BusinessService.Implementation;
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
        // GET: /Grades//for index
        public async Task<IActionResult> Index()
        {
             var model = await _gradeService.GetGrades();
            return View(model);
        }
       // GET: Grades/Create//this is for the create button 
        public IActionResult Create()
        {
            return View();
        }


        // GET: Grades/Create//this is for the edit
        public async Task<IActionResult> Edit(int Id)
        {
            var model = await _gradeService.GetGradeById(Id);
            return View(model);

        }


        // GET: GRADES/Delete/
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _gradeService.GetGradeById(id??0);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }





        // POST: STUDENTS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var model = await _gradeService.DeleteGrade(id??0);
            if (model == false)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));

        }

        // POST: Grades/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GradeViewModel model)
        {
            if (ModelState.IsValid)
            {
              model=  await _gradeService.CreateGrade(model); // Service saves to DB
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // POST: STUDENTS/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("GradeID,Name,Description")] GradeViewModel model)
        {
            if (id != model.GradeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _gradeService.UpdateGrade(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }
}