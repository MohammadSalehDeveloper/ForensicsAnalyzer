namespace ForensicsAnalyzer.Contracts.Cases;

public sealed class CreateCaseAssignmentRequest
{
    public Guid CaseId { get; set; }
    public string UserId { get; set; } = string.Empty;
}
