namespace ContosoUniversity.Models
{
    public enum Grade//represent a possibe grade a student can earn  , using enum makes the code to be readable and prevents invaid grades like Z
    {
        A, B, C, D, F//values 
    }

    public class Enrollment//defined a class called Enrollment 
    {
        public int EnrollmentID { get; set; }   // Primary key in the database , EnrollmentID identifies each enrollment record.
        public int CourseID { get; set; }       // Foreign key,Course ID links to Course Table 
        public int StudentID { get; set; }      // Foreign key,Student ID links to Student table

        public Grade? Grade { get; set; }       // Nullable Property , in this case ours is Grade? wich means property can hold a grade value or be null.Nullable because a student might not yet have a grade assigned.

        // Navigation properties
        public Course Course { get; set; } // Navigation property 
        public Student Student { get; set; }// Navigation property 

        //both of these are navigation Properties , Course and Student 
        //They let you move from an Enrollment object to its related Course and Student.
        //enrollment.Course.Title or enrollment.Student.LastName.
    }
}