using System;
using System.Collections.Generic;
using System.Text;
using BusinessObject;

namespace BusinessLogic.Interface
{
    /// <summary>
    /// /this is the interfeace for Grades 
    /// </summary>
    public interface IGradeService
    {

    /// <summary>
    /// /this is the methiod for all the grades (for main create button ) 
    /// </summary>
    /// <returns></returns>
       Task<IEnumerable<GradeViewModel>> GetGrades();

        /// <summary>
        /// this is for the method create grade 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
       Task <GradeViewModel> CreateGrade(GradeViewModel model);

        /// <summary>
        /// /this is the for get grade method 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       Task<GradeViewModel?> GetGradeById(int id);

        /// <summary>
        /// //this is  for update grade for id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<GradeViewModel?> UpdateGrade(GradeViewModel model); 

    }
}
