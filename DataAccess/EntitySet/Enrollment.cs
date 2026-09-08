namespace DataAccess.EntitySet
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }

       public int  GradeID { get; set; }

        public Course Course { get; set; }
        public Student Student { get; set; }
        public Grade Grade { get; set; }
    }
}
