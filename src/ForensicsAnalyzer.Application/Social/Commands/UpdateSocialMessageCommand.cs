using MediatR;

namespace ForensicsAnalyzer.Application.Social.Commands;

public record UpdateSocialMessageCommand(
    Guid Id,
    string Content,
    string? AttachmentUrl,
    string? AttachmentType,
    bool IsEdited,
    DateTime SentAt,
    DateTime? EditedAt,
    Guid? ReplyToMessageId
) : IRequest;
