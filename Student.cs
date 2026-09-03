using System;
using.System.Collections.Generic;
using.System.Text;

namespace DataAccess EntitySet
{
public class Student
{
        public int StudentID { get; set; }//Primary Key 

        public string LastName { get; set; }

        public string FirstMidName { get; set; }

        public DateTime EnrollmentDate { get; set; }

        public.ICollection<Enrollement> Enrollements { get; set; }
    }
}

}