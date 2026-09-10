namespace DataAccess.EntitySet
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
       public int  GradeID { get; set; }

        public string? GradeName { get; set; }
        public string? StudentName { get; set; }
        public string? CourseName { get; set; }
    }
}
