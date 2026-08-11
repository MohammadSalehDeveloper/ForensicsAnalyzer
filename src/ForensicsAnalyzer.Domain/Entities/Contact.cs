using ForensicsAnalyzer.Domain.Base;

namespace ForensicsAnalyzer.Domain.Entities;

public class Contact : BaseEntity
{
    public Guid ArtifactId { get; set; }
    
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Alias { get; set; }            // Nickname or known alias

    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }

    public string? Address { get; set; }
    public string? Organization { get; set; }     // Company or group
    public string? JobTitle { get; set; }

    public string? SocialMediaHandle { get; set; } // Username or profile link

    public string? Notes { get; set; }             // Investigator notes
    public string? Source { get; set; }            // Where this contact was discovered (device, chat app, etc.)

    public DateTime? LastContactedAt { get; set; }
    public bool IsSuspicious { get; set; }        // Flag used by investigators

}