using ForensicsAnalyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ForensicsAnalyzer.Infrastructure.Configurations;

public class SocialMemberConfiguration : IEntityTypeConfiguration<SocialMember>
{
    public void Configure(EntityTypeBuilder<SocialMember> builder)
    {
        builder.HasKey(m => m.Id);
        builder.Property(m => m.ChatId).IsRequired();
        builder.Property(m => m.UserId).IsRequired();
        builder.Property(m => m.Nickname).HasMaxLength(100);
        builder.Property(m => m.JoinedAt).IsRequired();

        builder.HasIndex(m => new { m.ChatId, m.UserId }).IsUnique();
    }
}
