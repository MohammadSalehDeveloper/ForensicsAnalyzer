namespace ForensicsAnalyzer.Contracts.Contacts;

public sealed class ContactDto
{
    public Guid Id { get; set; }
    public Guid ArtifactId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Alias { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? Organization { get; set; }
    public string? JobTitle { get; set; }
    public string? SocialMediaHandle { get; set; }
    public string? Notes { get; set; }
    public string? Source { get; set; }
    public DateTime? LastContactedAt { get; set; }
    public bool IsSuspicious { get; set; }
    public DateTime CreatedAt { get; set; }
}
