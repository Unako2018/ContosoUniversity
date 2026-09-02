using BusinessObject;

namespace BusinessService.Interface
{
    public interface IStudentService
    {
      Task<IEnumerable<StudentViewModel>> GetStudents();

       Task<StudentViewModel> GetStudentById(int id);
    }
}
