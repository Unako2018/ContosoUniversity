using BusinessObject;
using BusinessService.Interface;
using DataAccess.EntitySet;
using Microsoft.EntityFrameworkCore;

namespace BusinessService.Implementation
{
    public class StudentService : IStudentService
    {
        private readonly SchoolContext _context;

        public StudentService(SchoolContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StudentViewModel>> GetStudents()
        {
            var students = await _context.Students.ToListAsync();

            var model = students.Select(a => new StudentViewModel()
            {
                ID = a.ID,
                LastName = a.LastName,
                FirstMidName = a.FirstMidName,
                EnrollmentDate = a.EnrollmentDate,
            });

            return model;
        }

        public async Task<StudentViewModel> GetStudentById(int id)
        {
            var student = await _context.Students
             .FirstOrDefaultAsync(m => m.ID == id);

            if (student == null)
            {
                return null;
            }
            var model = new StudentViewModel()
            {
                ID = student.ID,
                LastName = student.LastName,
                FirstMidName = student.FirstMidName,
                EnrollmentDate = student.EnrollmentDate,
            };
            return model;
        }
    }
}
