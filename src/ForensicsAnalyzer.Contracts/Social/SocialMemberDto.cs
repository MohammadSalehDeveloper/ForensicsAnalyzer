namespace ForensicsAnalyzer.Contracts.Social;

public sealed class SocialMemberDto
{
    public Guid Id { get; set; }
    public Guid ChatId { get; set; }
    public Guid UserId { get; set; }
    public string? Nickname { get; set; }
    public bool IsAdmin { get; set; }
    public bool IsMuted { get; set; }
    public bool IsBlocked { get; set; }
    public DateTime JoinedAt { get; set; }
    public DateTime? LastSeenAt { get; set; }
    public DateTime CreatedAt { get; set; }
}
