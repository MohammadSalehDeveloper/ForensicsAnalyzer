using ForensicsAnalyzer.Domain.Base;

namespace ForensicsAnalyzer.Domain.Entities;

public class SocialMessenger : BaseEntity
{
    public required string Name { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public bool IsPublic { get; set; }

    public bool IsArchived { get; set; }

    public ICollection<SocialChat> Chats { get; set; } = new List<SocialChat>();
}