using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessObject
{
    public class EnrollmentViewModel
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        
        public string Grade { get; set; }

        public EnrollmentsViewModel Course { get; set; }
        public StudentViewModel Student { get; set; }

    }
}


