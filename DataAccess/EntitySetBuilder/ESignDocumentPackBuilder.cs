using DataAccess.Constant;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntitySet;

public class ESignDocumentPackBuilder : IEntityTypeConfiguration<ESignDocumentPack>
{
    public void Configure(EntityTypeBuilder<ESignDocumentPack> entityBuilder)
    {
        entityBuilder.ToTable(nameof(ESignDocumentPack), Schema.Esign);
        entityBuilder.HasKey(c => c.Id);

        entityBuilder.Property(c => c.DocumentPackKey).IsRequired();
        entityBuilder.Property(c => c.SigningLink).IsRequired();
        entityBuilder.HasIndex(c => new { c.DocumentPackKey, c.ProspectId }).IsUnique();

        entityBuilder
        .HasOne(e => e.Prospect)
        .WithMany(e => e.ESignDocumentPacks)
        .HasForeignKey(e => e.ProspectId)
        .OnDelete(DeleteBehavior.Cascade);

        entityBuilder
        .HasOne(e => e.ESignDocumentStatus)
        .WithMany(e => e.ESignDocumentPacks)
        .HasForeignKey(e => e.ESignDocumentStatusId)
        .OnDelete(DeleteBehavior.Cascade);
    }
}
