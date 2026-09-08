using System;
using System.Collections.Generic;
using System.Text;
using BusinessObject;
namespace BusinessLogic.Interface
{

    public interface ICourseService
    {
        Task<IEnumerable<CourseViewModel>> GetCourses();
        Task<CourseViewModel?> GetCourseById(int id);
        Task<CourseViewModel> CreateCourse(CourseViewModel model);
        Task<CourseViewModel?> UpdateCourse(CourseViewModel model);
        Task<bool> DeleteCourse(int id);


    }
}


