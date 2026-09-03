using System;
using.System.Collections.Generic;
using.System.Text;

namespace DataAccess EntitySet
{
public class Enrollment 


{
    public int EnrollementID { get; set; }//Primary Key 

    public int CourseID { get; set; }
    public int StudentID { get; set; }
    public string Grade { get; set; }

    public Course Course { get; set; }
    public Student Student { get; set; }
    public Grade Grade { get; set; }


}