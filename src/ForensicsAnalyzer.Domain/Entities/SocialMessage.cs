using ForensicsAnalyzer.Domain.Base;

namespace ForensicsAnalyzer.Domain.Entities;

public class SocialMessage : BaseEntity, ISoftDelete
{
    public Guid ChatId { get; set; }

    public Guid SenderId { get; set; }

    public required string Content { get; set; }

    public string? AttachmentUrl { get; set; }

    public string? AttachmentType { get; set; }

    public bool IsEdited { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime SentAt { get; set; }

    public DateTime? EditedAt { get; set; }

    // reply support
    public Guid? ReplyToMessageId { get; set; }

    // navigation
    public SocialChat Chat { get; set; } = null!;

    public SocialMember Sender { get; set; } = null!;

    public SocialMessage? ReplyToMessage { get; set; }
}