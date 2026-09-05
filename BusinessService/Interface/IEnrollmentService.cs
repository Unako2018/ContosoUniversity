using System;
using System.Collections.Generic;
using System.Text;
using BusinessObject;
namespace BusinessLogic.Interface
{
    public interface IEnrollmentService
    {
        Task<IEnumerable<EnrollmentViewModel>> GetEnrollments();
        Task<EnrollmentsViewModel?> GetEnrollmentById(int id);
        Task<EnrollmentsViewModel> CreateEnrollment(EnrollmentsViewModel model);
        Task<EnrollmentsViewModel?> UpdateEnrollment(EnrollmentsViewModel model);
        Task<bool> DeleteEnrollment(int id);

    }
}
