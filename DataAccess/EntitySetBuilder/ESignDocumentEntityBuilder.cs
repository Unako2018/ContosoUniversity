using DataAccess.Constant;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntitySet;

public class ESignDocumentEntityBuilder : IEntityTypeConfiguration<ESignDocumentEntity>
{
    public void Configure(EntityTypeBuilder<ESignDocumentEntity> entityBuilder)
    {
        entityBuilder.ToTable(nameof(ESignDocumentEntity), Schema.Esign);
        entityBuilder.HasKey(c => c.Id);

        entityBuilder.Property(c => c.EntityKey).IsRequired();
        entityBuilder.Property(c => c.DocumentName).IsRequired();
        entityBuilder.Property(c => c.ServePdfUrl).IsRequired();
        entityBuilder.HasIndex(c => new { c.EntityKey, c.ESignDocumentPackId }).IsUnique();

        entityBuilder
        .HasOne(e => e.ESignDocumentPack)
        .WithMany(e => e.ESignDocumentEntities)
        .HasForeignKey(e => e.ESignDocumentPackId)
        .OnDelete(DeleteBehavior.Cascade);

        entityBuilder
        .HasOne(e => e.ESignDocumentStatus)
        .WithMany(e => e.ESignDocumentEntities)
        .HasForeignKey(e => e.ESignDocumentStatusId)
        .OnDelete(DeleteBehavior.Cascade);
    }
}
