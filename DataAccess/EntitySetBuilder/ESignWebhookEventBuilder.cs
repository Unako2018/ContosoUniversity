using DataAccess.Constant;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntitySet;

public class ESignWebhookEventBuilder : IEntityTypeConfiguration<ESignWebhookEvent>
{
    public void Configure(EntityTypeBuilder<ESignWebhookEvent> entityBuilder)
    {
        entityBuilder.ToTable(nameof(ESignWebhookEvent), Schema.Esign);
        entityBuilder.HasKey(c => c.Id);

        entityBuilder.Property(c => c.Name).IsRequired();
        entityBuilder.Property(c => c.Name).HasMaxLength(15);
        entityBuilder.HasIndex(c => c.Name).IsUnique();
    }
}
