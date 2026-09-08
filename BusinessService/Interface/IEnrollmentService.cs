using System;
using System.Collections.Generic;
using System.Text;
using BusinessObject;
namespace BusinessLogic.Interface
{
    public interface IEnrollmentService
    {
        Task<IEnumerable<EnrollmentViewModel>> GetEnrollments();
        Task<EnrollmentViewModel?> GetEnrollmentById(int id);
        Task<EnrollmentViewModel> CreateEnrollment(EnrollmentViewModel model);
        Task<EnrollmentViewModel?> UpdateEnrollment(EnrollmentViewModel model);
        Task<bool> DeleteEnrollment(int id);
    }
}
