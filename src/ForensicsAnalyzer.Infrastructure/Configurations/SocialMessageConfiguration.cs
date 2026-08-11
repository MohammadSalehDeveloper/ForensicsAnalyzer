using ForensicsAnalyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ForensicsAnalyzer.Infrastructure.Configurations;

public class SocialMessageConfiguration : IEntityTypeConfiguration<SocialMessage>
{
    public void Configure(EntityTypeBuilder<SocialMessage> builder)
    {
        builder.HasKey(m => m.Id);
        builder.Property(m => m.ChatId).IsRequired();
        builder.Property(m => m.SenderId).IsRequired();
        builder.Property(m => m.Content).IsRequired().HasMaxLength(4000);
        builder.Property(m => m.AttachmentUrl).HasMaxLength(500);
        builder.Property(m => m.AttachmentType).HasMaxLength(100);
        builder.Property(m => m.SentAt).IsRequired();

        builder.HasOne(m => m.Sender)
            .WithMany()
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(m => m.ReplyToMessage)
            .WithMany()
            .HasForeignKey(m => m.ReplyToMessageId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
