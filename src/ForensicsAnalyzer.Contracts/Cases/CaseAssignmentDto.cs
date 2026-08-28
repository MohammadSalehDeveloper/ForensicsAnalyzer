namespace ForensicsAnalyzer.Contracts.Cases;

public sealed class CaseAssignmentDto
{
    public Guid Id { get; set; }
    public Guid CaseId { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string AssignedByUserId { get; set; } = string.Empty;
    public DateTime AssignedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
