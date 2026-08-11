using ForensicsAnalyzer.Domain.Base;

namespace ForensicsAnalyzer.Domain.Entities;

public class SocialChat:BaseEntity
{
    public Guid MessengerId { get; set; }

    public required string Title { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public bool IsGroup { get; set; }

    public bool IsPrivate { get; set; }

    public DateTime? LastMessageAt { get; set; }

    // navigation
    public SocialMessenger Messenger { get; set; } = null!;

    public ICollection<SocialMember> Members { get; set; } = new List<SocialMember>();

    public ICollection<SocialMessage> Messages { get; set; } = new List<SocialMessage>();
}