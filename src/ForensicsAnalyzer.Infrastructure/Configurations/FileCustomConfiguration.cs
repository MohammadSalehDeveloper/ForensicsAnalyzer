using ForensicsAnalyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ForensicsAnalyzer.Infrastructure.Configurations;

public class FileCustomConfiguration : IEntityTypeConfiguration<FileCustom>
{
    public void Configure(EntityTypeBuilder<FileCustom> builder)
    {
        builder.HasKey(f => f.Id);
        builder.Property(f => f.FileName).IsRequired().HasMaxLength(255);
        builder.Property(f => f.Extension).IsRequired().HasMaxLength(20);
        builder.Property(f => f.FullPath).IsRequired().HasMaxLength(1000);
        builder.Property(f => f.DirectoryPath).IsRequired().HasMaxLength(1000);
        builder.Property(f => f.SourceImage).IsRequired().HasMaxLength(500);
        builder.Property(f => f.MimeType).IsRequired().HasMaxLength(100);
        builder.Property(f => f.Category).IsRequired().HasMaxLength(100);
        builder.Property(f => f.Md5Hash).HasMaxLength(32);
        builder.Property(f => f.Sha1Hash).HasMaxLength(40);
        builder.Property(f => f.Sha256Hash).HasMaxLength(64);
        builder.Property(f => f.Owner).HasMaxLength(200);
        builder.Property(f => f.Permissions).HasMaxLength(100);
        builder.Property(f => f.SuspiciousReason).HasMaxLength(500);
        builder.Property(f => f.ExtractedFrom).HasMaxLength(500);

        builder.HasMany<Thumbnail>()
            .WithOne()
            .HasForeignKey(t => t.FileCustomId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany<LocationImage>()
            .WithOne()
            .HasForeignKey(li => li.FileCustomId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
