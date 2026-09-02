using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
    public class Course
    {
        public int CourseID { get; set; } // Primary Key
        public string Title { get; set; } // Course title
        public int Credits { get; set; }  // Course credits
    }
}