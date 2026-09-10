using System.ComponentModel.DataAnnotations;

namespace BusinessObject
{
    public class StudentViewModel
    {
        public int ID { get; set; }

        [Display(Name = "Surname")]
        [Required(ErrorMessage = "required")]
        [StringLength(100, ErrorMessage = "must be less than 100 characters.")]
        public string LastName { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "required")]
        [StringLength(100, ErrorMessage = "must be less than 100 characters.")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        [StringLength(100, ErrorMessage = "must be less than 100 characters.")]
        public string FirstMidName { get; set; }


        [DataType(DataType.Date)]
        [Display(Name = "Enrollment Date")]
        public DateTime EnrollmentDate { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Contact Number")]
        [Required(ErrorMessage = "required")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "format is not valid.")]
        public string? ContactNo { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "required")]
        [EmailAddress(ErrorMessage = "format is not valid")]
        public string? Email { get; set; }

        public ICollection<EnrollmentViewModel> Enrollments { get; set; }
    }
}