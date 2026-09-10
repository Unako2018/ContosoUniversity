using BusinessLogic.Interface;
using BusinessObject;
using BusinessService.Interface;
using DataAccess.EntitySet;
using Microsoft.EntityFrameworkCore;
namespace BusinessService.Implementation
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly SchoolContext _context;

        public EnrollmentService(SchoolContext context)
        {
            _context = context;
        }
        // Get all Enrollments
        public async Task<IEnumerable<EnrollmentViewModel>> GetEnrollments()
        {
            var enrollments = await _context.Enrollments.Include(e => e.GradeName)
                                                        .Include(e => e.CourseName)
                                                        .Include(e => e.StudentName)
                .ToListAsync();
            var model = enrollments.Select(e => new EnrollmentViewModel()
            {
                CourseName = e.CourseName,
                StudentName = e.StudentName,
                GradeName = e.GradeName

            });
            return model;
        }

        // Get enrollment by Names /not nullable 
        public async Task<EnrollmentViewModel?> GetEnrollmentByName(string name)
        {
            var enrollment = await _context.Enrollments
                .Include(e => e.StudentName)
                .Include(e => e.CourseName)
                 .Include(e => e.GradeName)
                .FirstOrDefaultAsync(e => e.StudentName == name);

            if (enrollment == null)
            {
                return null;
            }

            var model = new EnrollmentViewModel
            {
                StudentName = enrollment.StudentName,
                CourseName = enrollment.CourseName,
                GradeName = enrollment.GradeName,
            };

            return model;
        }
        // Create new Enrollment
        public async Task<EnrollmentViewModel> CreateEnrollment(EnrollmentViewModel model)
        {
            var enrollment = new Enrollment()
            {
                GradeName = model.GradeName,
                StudentName = model.StudentName,
                CourseName = model.CourseName,
            };
            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();
            // update with generated ID
            return model;
        }

        // Update Enrollment
        public async Task<EnrollmentViewModel?> UpdateEnrollment(EnrollmentViewModel model)
        {
            var enrollment = await _context.Enrollments.FindAsync(model.EnrollmentID);
            if (enrollment == null)
            {
                return null;
            }

            enrollment.GradeName = model.GradeName;


            _context.Enrollments.Update(enrollment);
            await _context.SaveChangesAsync();

            return model;
        }
        // Delete Enrollment
        public async Task<bool> DeleteEnrollment(int id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return false;
            }

            _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<EnrollmentViewModel?> GetEnrollmentById(int id)
        {
            throw new NotImplementedException();
        }
    }
}



  