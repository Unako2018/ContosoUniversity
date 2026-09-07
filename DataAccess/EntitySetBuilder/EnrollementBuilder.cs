using DataAccess.EntitySet;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntitySetBuilder
{
    public class EnrollmentBuilder : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> entityBuilder)
        {
            entityBuilder.ToTable(nameof(Enrollment));
            entityBuilder.HasKey(c => c.EnrollmentID);

            entityBuilder.Property(c => c.CourseID).IsRequired();
            entityBuilder.Property(c => c.StudentID).IsRequired();


            entityBuilder
            .HasOne(e => e.Student)
            .WithMany(e => e.Enrollments)
            .HasForeignKey(e => e.StudentID)
            .OnDelete(DeleteBehavior.Cascade);

            //entityBuilder
            //.HasOne(e => e.Course)
            //.WithMany(e => e.Enrollments)
            //.HasForeignKey(e => e.CourseID)
            //.OnDelete(DeleteBehavior.Cascade);
        }
    }
}
