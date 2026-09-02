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
        
        //To replace with class object
        public string Grade { get; set; }

        public CourseViewModel Course { get; set; }
        public StudentViewModel Student { get; set; }
    }
}
