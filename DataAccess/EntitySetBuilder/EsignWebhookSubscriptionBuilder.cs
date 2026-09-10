using DataAccess.Constant;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntitySet;

public class EsignWebhookSubscriptionBuilder : IEntityTypeConfiguration<ESignWebhookSubscription>
{
    public void Configure(EntityTypeBuilder<ESignWebhookSubscription> entityBuilder)
    {
        entityBuilder.ToTable(nameof(ESignWebhookSubscription), Schema.Esign);
        entityBuilder.HasKey(c => c.Id);

        entityBuilder.Property(c => c.ESignDocumentEntityId).IsRequired();

        entityBuilder.Property(c => c.ProspectId).IsRequired();

        entityBuilder
        .HasOne(e => e.Prospect)
        .WithMany(e => e.EsignWebhookSubscriptions)
        .HasForeignKey(e => e.ProspectId);

        entityBuilder
        .HasOne(e => e.ESignDocumentEntity)
        .WithMany(e => e.ESignWebhookSubscriptions)
        .HasForeignKey(e => e.ESignDocumentEntityId)
        .OnDelete(DeleteBehavior.Cascade);

    }
}
