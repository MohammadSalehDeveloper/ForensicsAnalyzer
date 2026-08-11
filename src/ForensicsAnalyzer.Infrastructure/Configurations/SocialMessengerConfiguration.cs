using ForensicsAnalyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ForensicsAnalyzer.Infrastructure.Configurations;

public class SocialMessengerConfiguration : IEntityTypeConfiguration<SocialMessenger>
{
    public void Configure(EntityTypeBuilder<SocialMessenger> builder)
    {
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Name).IsRequired().HasMaxLength(100);
        builder.Property(m => m.Description).HasMaxLength(500);
        builder.Property(m => m.ImageUrl).HasMaxLength(500);

        builder.HasMany(m => m.Chats)
            .WithOne(c => c.Messenger)
            .HasForeignKey(c => c.MessengerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
