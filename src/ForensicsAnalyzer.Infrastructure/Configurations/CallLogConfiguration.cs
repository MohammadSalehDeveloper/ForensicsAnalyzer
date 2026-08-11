using ForensicsAnalyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ForensicsAnalyzer.Infrastructure.Configurations;

public class CallLogConfiguration : IEntityTypeConfiguration<CallLog>
{
    public void Configure(EntityTypeBuilder<CallLog> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.ArtifactId).IsRequired();
        builder.Property(c => c.PhoneNumber).HasMaxLength(50);
        builder.Property(c => c.ContactName).HasMaxLength(200);
        builder.Property(c => c.DeviceId).HasMaxLength(100);
        builder.Property(c => c.Imei).HasMaxLength(50);
        builder.Property(c => c.SimSerialNumber).HasMaxLength(50);
        builder.Property(c => c.CarrierName).HasMaxLength(100);
        builder.Property(c => c.SourceApplication).HasMaxLength(100);
        builder.Property(c => c.CellTowerId).HasMaxLength(100);
        builder.Property(c => c.RecordIdentifier).HasMaxLength(200);
        builder.Property(c => c.DatabaseSource).HasMaxLength(200);
    }
}
