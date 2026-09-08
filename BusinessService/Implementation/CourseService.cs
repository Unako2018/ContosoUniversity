using BusinessLogic.Interface;
using BusinessObject;
using BusinessService.Interface;
using DataAccess.EntitySet;
using Microsoft.EntityFrameworkCore;
namespace BusinessService.Implementation
{
    public class CourseService : ICourseService
    {
        private readonly SchoolContext _courseservice;

        public CourseService(SchoolContext courseservice)
        {
            _courseservice = courseservice;
        }
        // Get all courses
        public async Task<IEnumerable<CourseViewModel>> GetCourses()
        {
            var courses = await _courseservice.Courses.ToListAsync();
            var model = courses.Select(c => new CourseViewModel()
            {
                CourseID = c.CourseID,
                Title = c.Title,
                Credits = c.Credits,
            });

            return model;
        }

        // Get course by ID
        public async Task<CourseViewModel?> GetCourseById(int id)
        {
            var course = await _courseservice.Courses
                .FirstOrDefaultAsync(m => m.CourseID == id);

            if (course == null)
            {
                return null;
            }

            var model = new CourseViewModel()
            {
                CourseID = course.CourseID,
                Title = course.Title,
                Credits = course.Credits
            };

            return model;
        }

        // Create new course
        public async Task<CourseViewModel> CreateCourse(CourseViewModel model)
        {
            var course = new Course()
            {
                Title = model.Title,
                Credits = model.Credits
            };

            _courseservice.Courses.Add(course);
            await _courseservice.SaveChangesAsync();

            model.CourseID = course.CourseID; // update with generated ID
            return model;
        }

        // Update course
        public async Task<CourseViewModel?> UpdateCourse(CourseViewModel model)
        {
            var course = await _courseservice.Courses.FindAsync(model.CourseID);
            if (course == null)
            {
                return null;
            }

            course.Title = model.Title;
            course.Credits = model.Credits;

            _courseservice.Courses.Update(course);
            await _courseservice.SaveChangesAsync();

            return model;
        }
        // Delete course
        public async Task<bool> DeleteCourse(int id)
        {
            var course = await _courseservice.Courses.FindAsync(id);
            if (course == null)
            {
                return false;
            }

            _courseservice.Courses.Remove(course);
            await _courseservice.SaveChangesAsync();
            return true;
        }
    }
}