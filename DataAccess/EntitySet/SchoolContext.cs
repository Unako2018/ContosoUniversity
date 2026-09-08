using Microsoft.EntityFrameworkCore;

namespace DataAccess.EntitySet
{
    public class EnrollmentService : DbContext
    {
        public EnrollmentService(DbContextOptions<EnrollmentService> options) : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().ToTable("Courses");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Grade>().ToTable("Grade");
        }
    }
}