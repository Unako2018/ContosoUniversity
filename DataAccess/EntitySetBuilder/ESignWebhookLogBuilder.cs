using DataAccess.Constant;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntitySet;

public class ESignWebhookLogBuilder : IEntityTypeConfiguration<ESignWebhookLog>
{
    public void Configure(EntityTypeBuilder<ESignWebhookLog> entityBuilder)
    {
        entityBuilder.ToTable(nameof(ESignWebhookLog), Schema.Esign);
        entityBuilder.HasKey(c => c.Id);

        entityBuilder.Property(c => c.CorrelationId).IsRequired();

        entityBuilder.Property(c => c.ProspectId).IsRequired();
        entityBuilder.Property(c => c.ESignWebHookEventId).IsRequired();

        entityBuilder
        .HasOne(e => e.ESignWebhookEvent)
        .WithMany(e => e.ESignWebhookLogs)
        .HasForeignKey(e => e.ESignWebHookEventId);

        entityBuilder
        .HasOne(e => e.Prospect)
        .WithMany(e => e.ESignWebhookLogs)
        .HasForeignKey(e => e.ESignWebHookEventId)
        .OnDelete(DeleteBehavior.Cascade);
    }
}
