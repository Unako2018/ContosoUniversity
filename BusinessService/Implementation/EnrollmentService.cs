using BusinessLogic.Interface;
using BusinessObject;
using BusinessService.Interface;
using DataAccess.EntitySet;
using Microsoft.EntityFrameworkCore;
namespace BusinessService.Implementation
{
    public class EnrollmntService : IEnrollmentService
    {
        private readonly SchoolContext _context;

        public EnrollmntService(SchoolContext context)
        {
            _context = context;
        }
        // Get all Enrollments
        public async Task<IEnumerable<EnrollmentViewModel>> GetEnrollments()
        {
            var enrollments = await _context.Enrollments.ToListAsync();
            var model = enrollments.Select(e => new EnrollmentsViewModel()
            {
                EnrollmentID = e.EnrollmentID,
                CourseID = e.CourseID,
                StudentID = e.StudentID,
                Grade = e.Grade,
            });
            return model;
        }

        // Get enrollment by ID
        public async Task<EnrollmentsViewModel?> GetEnrollmentById(int id)
        {
            var enrollment = await _context.Enrollments
                .FirstOrDefaultAsync(m => m.EnrollmentID == id);

            if (enrollment == null)
            {
                return null;
            }

            var model = new EnrollmentsViewModel()
            {
                EnrollmentID = enrollment.EnrollmentID,
                CourseID = enrollment.CourseID,
                StudentID = enrollment.StudentID,
                Grade = enrollment.Grade,

            };

            return model;
        }

        // Create new Enrollment
        public async Task<EnrollmentsViewModel> CreateEnrollment(EnrollmentsViewModel model)
        {
            var enrollment = new Enrollment()
            {
                Grade = model.Grade,
           
            };

            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();

            model.EnrollmentID = enrollment.EnrollmentID;
            model.CourseID = enrollment.CourseID;
            model.StudentID = enrollment.StudentID;
            // update with generated ID
            return model;
        }

        // Update Enrollment
        public async Task<EnrollmentsViewModel?> UpdateEnrollment(EnrollmentsViewModel model)
        {
            var enrollment = await _context.Enrollments.FindAsync(model.EnrollmentID);
            if (enrollment == null)
            {
                return null;
            }

            enrollment.Grade = model.Grade;
          

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

        public Task<IEnumerable<GradeViewModel>> GetGrades()
        {
            throw new NotImplementedException();
        }

        public Task<GradeViewModel> CreateGrade(GradeViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<GradeViewModel?> GetGradeById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<GradeViewModel?> UpdateGrade(GradeViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteGrade(int id)
        {
            throw new NotImplementedException();
        }
    }
}