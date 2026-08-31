using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
    public class Course
    {
        public int CourseID { get; set; }//Primary Key 
        public string Title { get; set; }//string ,stores the name or title of the course 
        public int Credits { get; set; }//Integer , stores the no of credits worth the course 
    }
}
