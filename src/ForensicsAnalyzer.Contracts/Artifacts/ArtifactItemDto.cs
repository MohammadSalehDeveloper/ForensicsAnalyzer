using ForensicsAnalyzer.Domain.Enums;

namespace ForensicsAnalyzer.Contracts.Artifacts;

public sealed class ArtifactItemDto
{
    public Guid Id { get; set; }
    public Guid ArtifactId { get; set; }
    public ArtifactType Type { get; set; }
    public Guid ReferenceId { get; set; }
    public DateTime CreatedAt { get; set; }
}
