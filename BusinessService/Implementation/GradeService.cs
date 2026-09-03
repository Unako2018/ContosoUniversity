using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using BusinessLogic.Interface;
using BusinessObject;
using DataAccess.EntitySet;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Implementation
{
    public class GradeService: IGradeService
    {
        private readonly SchoolContext _schoolContext;
        public GradeService(SchoolContext schoolContext)
        {
            _schoolContext = schoolContext;
        }

        public async Task<GradeViewModel> CreateGrade(GradeViewModel model)
        {
            var grade = new Grade();
            grade.Name = model.Name;
            grade.Description = model.Description;

            _schoolContext.Add(grade);
           await _schoolContext.SaveChangesAsync();
            return model;
        }

        public async Task<IEnumerable<GradeViewModel>> GetGrades()
        {
            var grades = await _schoolContext.Grades.ToListAsync();

            var model = grades.Select(a => new GradeViewModel()
            {
                GradeID = a.GradeID,
                Name = a.Name,
                Description = a.Description

            });

            return model;
        }
    }

}
