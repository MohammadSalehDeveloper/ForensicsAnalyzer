using ForensicsAnalyzer.Domain.Enums;

namespace ForensicsAnalyzer.Contracts.Artifacts;

public sealed class ArtifactDto
{
    public Guid Id { get; set; }
    public Guid CaseId { get; set; }
    public string Name { get; set; } = string.Empty;
    public ArtifactType Type { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
