using BusinessLogic.Interface;
using BusinessObject;
using BusinessService.Interface;
using DataAccess.EntitySet;
using Microsoft.EntityFrameworkCore;

namespace BusinessService.Implementation
{
    public class GradeService : IGradeService
    {
        private readonly SchoolContext _context;

        public GradeService(SchoolContext context)
        {
            _context = context;
        }

        // Get all grades
        public async Task<IEnumerable<GradeViewModel>> GetGrades()
        {
            var grades = await _context.Grades.ToListAsync();

            var model = grades.Select(g => new GradeViewModel()
            {
                GradeID = g.GradeID,
                Name = g.Name,
                Description = g.Description
            });

            return model;
        }

        // Get grade by ID
        public async Task<GradeViewModel?> GetGradeById(int id)
        {
            var grade = await _context.Grades
                .FirstOrDefaultAsync(m => m.GradeID == id);

            if (grade == null)
            {
                return null;
            }

            var model = new GradeViewModel()
            {
                GradeID = grade.GradeID,
                Name = grade.Name,
                Description = grade.Description
            };

            return model;
        }

        // Create a new grade
        public async Task<GradeViewModel> CreateGrade(GradeViewModel model)
        {
            var grade = new Grade()
            {
                Name = model.Name,
                Description = model.Description
            };

            _context.Grades.Add(grade);
            await _context.SaveChangesAsync();

            model.GradeID = grade.GradeID; // update with generated ID
            return model;
        }

        // Update an existing grade
        public async Task<GradeViewModel?> UpdateGrade(GradeViewModel model)
        {
            var grade = await _context.Grades.FindAsync(model.GradeID);
            if (grade == null)
            {
                return null;
            }

            grade.Name = model.Name;
            grade.Description = model.Description;

            _context.Grades.Update(grade);
            await _context.SaveChangesAsync();

            return model;
        }

        // Delete a grade
        public async Task<bool> DeleteGrade(int id)
        {
            var grade = await _context.Grades.FindAsync(id);
            if (grade == null)
            {
                return false;
            }

            _context.Grades.Remove(grade);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
