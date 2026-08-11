using ForensicsAnalyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ForensicsAnalyzer.Infrastructure.Configurations;

public class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.HasKey(l => l.Id);
        builder.Property(l => l.ArtifactId).IsRequired();
        builder.Property(l => l.Latitude).IsRequired();
        builder.Property(l => l.Longitude).IsRequired();
        builder.Property(l => l.Country).HasMaxLength(100);
        builder.Property(l => l.State).HasMaxLength(100);
        builder.Property(l => l.City).HasMaxLength(100);
        builder.Property(l => l.Street).HasMaxLength(200);
        builder.Property(l => l.PostalCode).HasMaxLength(20);
        builder.Property(l => l.FullAddress).HasMaxLength(500);
        builder.Property(l => l.CellTowerId).HasMaxLength(100);
        builder.Property(l => l.MobileCountryCode).HasMaxLength(10);
        builder.Property(l => l.MobileNetworkCode).HasMaxLength(10);
        builder.Property(l => l.LocationAreaCode).HasMaxLength(20);
        builder.Property(l => l.NetworkProvider).HasMaxLength(100);
        builder.Property(l => l.WifiSSID).HasMaxLength(100);
        builder.Property(l => l.WifiBSSID).HasMaxLength(50);
        builder.Property(l => l.IpAddress).HasMaxLength(45);
    }
}
