using ForensicsAnalyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ForensicsAnalyzer.Infrastructure.Configurations;

public class LocationImageConfiguration : IEntityTypeConfiguration<LocationImage>
{
    public void Configure(EntityTypeBuilder<LocationImage> builder)
    {
        builder.HasKey(li => li.Id);
        builder.Property(li => li.FileCustomId).IsRequired();
        builder.Property(li => li.Latitude).IsRequired();
        builder.Property(li => li.Longitude).IsRequired();
        builder.Property(li => li.Description).HasMaxLength(500);

        // Ignore the navigation to Location (LocationImage references FileCustom, not Location)
        builder.Ignore(li => li.Location);
    }
}
