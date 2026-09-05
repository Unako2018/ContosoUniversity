using BusinessLogic.Interface;
using BusinessObject;
using BusinessService.Interface;
using DataAccess.EntitySet;
using Microsoft.EntityFrameworkCore;
namespace BusinessService.Implementation
{
    public class CourseService : ICourseService
    {
        private readonly SchoolContext _context;

        public CourseService(SchoolContext context)
        {
            _context = context;
        }
        // Get all courses
        public async Task<IEnumerable<EnrollmentsViewModel>> GetCourses()
        {
            var courses = await _context.Courses.ToListAsync();
            var model = courses.Select(c => new EnrollmentsViewModel()
            {
                EnrollmentID = c.CourseID,
                Title = c.Title,
                Credits = c.Credits,
            });

            return model;
        }

        // Get course by ID
        public async Task<EnrollmentsViewModel?> GetCourseById(int id)
        {
            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.CourseID == id);

            if (course == null)
            {
                return null;
            }

            var model = new EnrollmentsViewModel()
            {
                EnrollmentID = course.CourseID,
                Title = course.Title,
                Credits = course.Credits
            };

            return model;
        }

        // Create new course
        public async Task<EnrollmentsViewModel> CreateCourse(EnrollmentsViewModel model)
        {
            var course = new Course()
            {
                Title = model.Title,
                Credits = model.Credits
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            model.EnrollmentID = course.CourseID; // update with generated ID
            return model;
        }

        // Update course
        public async Task<EnrollmentsViewModel?> UpdateCourse(EnrollmentsViewModel model)
        {
            var course = await _context.Courses.FindAsync(model.EnrollmentID);
            if (course == null)
            {
                return null;
            }

            course.Title = model.Title;
            course.Credits = model.Credits;

            _context.Courses.Update(course);
            await _context.SaveChangesAsync();

            return model;
        }
        // Delete course
        public async Task<bool> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return false;
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}