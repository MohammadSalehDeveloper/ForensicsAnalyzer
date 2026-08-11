using ForensicsAnalyzer.Domain.Base;
using ForensicsAnalyzer.Domain.Enums;

namespace ForensicsAnalyzer.Domain.Entities;

public class ArtifactItem:BaseEntity
{
    public Guid ArtifactId { get; set; }
    
    public ArtifactType Type { get; set; }

    public Guid ReferenceId { get; set; }

    // navigation
    public Artifact Artifact { get; set; } = null!;
}