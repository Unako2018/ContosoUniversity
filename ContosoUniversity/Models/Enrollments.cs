namespace ContosoUniversity.Models
{
    public enum Grade
    {
        A, B, C, D, F
    }

    public class Enrollment
    {
        public int EnrollmentID { get; set; }   // Primary key
        public int CourseID { get; set; }       // Foreign key
        public int StudentID { get; set; }      // Foreign key

        public Grade? Grade { get; set; }       // Nullable grade

        // Navigation properties
        public Course Course { get; set; }
        public Student Student { get; set; }
    }
}