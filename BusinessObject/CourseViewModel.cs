namespace BusinessObject
{
    public class EnrollmentsViewModel
    {
        public int EnrollmentID { get; set; }//Primary Key 
        public string Title { get; set; }//string ,stores the name or title of the course 
        public int Credits { get; set; }//Integer , stores the no of credits worth the course 
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public string Grade { get; set; }
    }
}
