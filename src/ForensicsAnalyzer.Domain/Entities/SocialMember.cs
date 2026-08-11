using ForensicsAnalyzer.Domain.Base;

namespace ForensicsAnalyzer.Domain.Entities;

public class SocialMember : BaseEntity
{
    public Guid ChatId { get; set; }

    public Guid UserId { get; set; }

    public string? Nickname { get; set; }

    public bool IsAdmin { get; set; }

    public bool IsMuted { get; set; }

    public bool IsBlocked { get; set; }

    public DateTime JoinedAt { get; set; }

    public DateTime? LastSeenAt { get; set; }

    // navigation
    public SocialChat Chat { get; set; } = null!;
}