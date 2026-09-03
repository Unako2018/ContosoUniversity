using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessObject
{
    public class GradeViewModel
    {
        public int GradeID { get; set; }        // Primary Key
        public string Name { get; set; }        // e.g. "A", "B", "C", "D", "F"
        public string Description { get; set; } // Optional, e.g. "Excellent", "Fail"

        // Navigation property
        public ICollection<EnrollmentViewModel> Enrollments { get; set; }
    }
}
