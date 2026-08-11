using ForensicsAnalyzer.Domain.Base;

namespace ForensicsAnalyzer.Domain.Entities;

public class Case : BaseEntity, ISoftDelete
{
    public Guid SourceId { get; set; }
    public string UserId { get; set; } = string.Empty;
    
    public required string Name { get; set; }
    
    public string? Description { get; set; }

    public bool IsDeleted { get; set; }
    
    // navigation
    public Source Source { get; set; } = null!;
    public ICollection<CaseAssignment> Assignments { get; set; } = new List<CaseAssignment>();
}
