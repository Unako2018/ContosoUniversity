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
            var enrollments = await _context.Enrollments.Include(e => e.Grade)
                                                        .Include(e => e.Course)
                                                        .Include(e => e.Student)
                .ToListAsync();
            var model = enrollments.Select(e => new EnrollmentViewModel()
            {
                EnrollmentID = e.EnrollmentID,
                CourseName = e.Course.Title,
                StudentName = e.Student.FirstMidName,
                GradeName = e.Grade.Name

            });
            return model;
        }

        // Get enrollment by ID
        public async Task<EnrollmentViewModel?> GetEnrollmentById(int id)
        {
            var enrollment = await _context.Enrollments
                .FirstOrDefaultAsync(m => m.EnrollmentID == id);

            if (enrollment == null)
            {
                return null;
            }

            var model = new EnrollmentViewModel()
            {
                EnrollmentID = enrollment.EnrollmentID,
                CourseID = enrollment.CourseID,
                StudentID = enrollment.StudentID,
                GradeID = enrollment.GradeID,

            };

            return model;
        }

        // Create new Enrollment
        public async Task<EnrollmentViewModel> CreateEnrollment(EnrollmentViewModel model)
        {
            var enrollment = new Enrollment()
            {
                GradeID = model.GradeID,
                StudentID = model.StudentID,
                CourseID = model.CourseID,
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

            enrollment.GradeID = model.GradeID;


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

    }
}



  