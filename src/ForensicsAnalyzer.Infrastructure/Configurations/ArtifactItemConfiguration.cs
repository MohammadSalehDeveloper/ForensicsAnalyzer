using ForensicsAnalyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ForensicsAnalyzer.Infrastructure.Configurations;

public class ArtifactItemConfiguration : IEntityTypeConfiguration<ArtifactItem>
{
    public void Configure(EntityTypeBuilder<ArtifactItem> builder)
    {
        builder.HasKey(i => i.Id);
        builder.Property(i => i.ArtifactId).IsRequired();
        builder.Property(i => i.Type).IsRequired();
        builder.Property(i => i.ReferenceId).IsRequired();
    }
}
