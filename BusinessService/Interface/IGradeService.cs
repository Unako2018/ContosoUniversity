using System;
using System.Collections.Generic;
using System.Text;
using BusinessObject;

namespace BusinessLogic.Interface
{
    public interface IGradeService
    {
       Task<IEnumerable<GradeViewModel>> GetGrades();
       Task <GradeViewModel> CreateGrade(GradeViewModel model);
    }
}
