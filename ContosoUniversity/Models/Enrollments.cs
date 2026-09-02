namespace ContosoUniversity.Models
{
    public enum Grade
    {
        A, B, C, D, F
    }

    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }

        public Grade? Grade { get; set; }   // enum property

        public Course Course { get; set; }
        public Student Student { get; set; }
        public int GradeID { get; internal set; }
    }
}