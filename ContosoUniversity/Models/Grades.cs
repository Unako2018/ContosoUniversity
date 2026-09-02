using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
    [Table("Grade")] // Maps to the Grade table in the database
    public class Grades
    {
        [Key] // Explicitly mark as Primary Key
        public int GradeID { get; set; }

        [Required]
        [StringLength(1)] // "A", "B", "C", "D", "F"
        public string Name { get; set; }

        [StringLength(50)] // Optional description like "Excellent", "Good", etc.
        public string Description { get; set; }

        // Navigation property: one grade can apply to many enrollments
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}