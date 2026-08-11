using ForensicsAnalyzer.Domain.Base;

namespace ForensicsAnalyzer.Domain.Entities;

public class CaseAssignment : BaseEntity
{
    public Guid CaseId { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string AssignedByUserId { get; set; } = string.Empty;
    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;

    // navigation
    public Case Case { get; set; } = null!;
}
