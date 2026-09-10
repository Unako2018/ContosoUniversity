using DataAccess.Constant;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntitySet;

public class ESignDocumentStatusBuilder : IEntityTypeConfiguration<ESignDocumentStatus>
{
    public void Configure(EntityTypeBuilder<ESignDocumentStatus> entityBuilder)
    {
        entityBuilder.ToTable(nameof(ESignDocumentStatus), Schema.Esign);
        entityBuilder.HasKey(c => c.Id);

        entityBuilder.Property(c => c.Name).IsRequired();
        entityBuilder.Property(c => c.Name).HasMaxLength(20);
        entityBuilder.HasIndex(c => c.Name).IsUnique();

        entityBuilder.Property(c => c.Key).IsRequired();
        entityBuilder.Property(c => c.Key).HasMaxLength(20);
        entityBuilder.HasIndex(c => c.Key).IsUnique();

        entityBuilder.Property(c => c.Key).HasMaxLength(100);

    }
}
