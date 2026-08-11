using ForensicsAnalyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ForensicsAnalyzer.Infrastructure.Configurations;

public class SocialChatConfiguration : IEntityTypeConfiguration<SocialChat>
{
    public void Configure(EntityTypeBuilder<SocialChat> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.MessengerId).IsRequired();
        builder.Property(c => c.Title).IsRequired().HasMaxLength(200);
        builder.Property(c => c.Description).HasMaxLength(500);
        builder.Property(c => c.ImageUrl).HasMaxLength(500);

        builder.HasMany(c => c.Members)
            .WithOne(m => m.Chat)
            .HasForeignKey(m => m.ChatId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.Messages)
            .WithOne(m => m.Chat)
            .HasForeignKey(m => m.ChatId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
