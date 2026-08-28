using MediatR;

namespace ForensicsAnalyzer.Application.Social.Commands;

public record CreateSocialMessageCommand(
    Guid ChatId,
    Guid SenderId,
    string Content,
    string? AttachmentUrl,
    string? AttachmentType,
    DateTime SentAt,
    Guid? ReplyToMessageId
) : IRequest<Guid>;
