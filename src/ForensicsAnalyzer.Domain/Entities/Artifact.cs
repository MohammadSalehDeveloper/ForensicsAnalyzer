using ForensicsAnalyzer.Domain.Base;
using ForensicsAnalyzer.Domain.Enums;

namespace ForensicsAnalyzer.Domain.Entities;

public class Artifact : BaseEntity
{
    public Guid CaseId { get; set; }
    
    public required string Name { get; set; }

    public ArtifactType Type { get; set; }

    // navigation
    public Case Case { get; set; } = null!;
    
    public ICollection<ArtifactItem> Items { get; set; } = new List<ArtifactItem>();
}