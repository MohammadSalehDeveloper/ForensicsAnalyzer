using ForensicsAnalyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ForensicsAnalyzer.Infrastructure.Configurations;

public class ThumbnailConfiguration : IEntityTypeConfiguration<Thumbnail>
{
    public void Configure(EntityTypeBuilder<Thumbnail> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.FileCustomId).IsRequired();
        builder.Property(t => t.ThumbnailPath).IsRequired().HasMaxLength(500);
        builder.Property(t => t.Width).IsRequired();
        builder.Property(t => t.Height).IsRequired();
        builder.Property(t => t.SizeLabel).HasMaxLength(50);
    }
}
