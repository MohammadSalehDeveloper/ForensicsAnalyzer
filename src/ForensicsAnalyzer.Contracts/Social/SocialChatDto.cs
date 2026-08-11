namespace ForensicsAnalyzer.Contracts.Social;

public sealed class SocialChatDto
{
    public Guid Id { get; set; }
    public Guid MessengerId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsGroup { get; set; }
    public bool IsPrivate { get; set; }
    public DateTime? LastMessageAt { get; set; }
    public DateTime CreatedAt { get; set; }
}
