using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.EntitySet
{
    public class Grade
    {
        public int GradeID { get; set; }        // Primary Key
        public string Name { get; set; }        // e.g. "A", "B", "C", "D", "F"
        public string Description { get; set; } // Optional, e.g. "Excellent", "Fail"

        // Navigation property
        public ICollection<Enrollment> Enrollments { get; set; }
    }

}


  