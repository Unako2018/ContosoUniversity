using DataAccess.EntitySet;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntitySetBuilder
{
    public class GradeBuilder : IEntityTypeConfiguration<Grade>
    {
        public void Configure(EntityTypeBuilder<Grade> entityBuilder)
        {
            entityBuilder.ToTable(nameof(Grade));
            entityBuilder.HasKey(c => c.GradeID);

            entityBuilder.Property(c => c.Name).IsRequired();

            entityBuilder.Property(c => c.Name).HasMaxLength(50);
            entityBuilder.Property(c => c.Description).HasMaxLength(100);

            //entityBuilder
            //.HasOne(e => e.GradeSubType)
            //.WithMany(e => e.Gradees)
            //.HasForeignKey(e => e.GradeSubTypeId)
            //.OnDelete(DeleteBehavior.Cascade);
        }
    }
}
