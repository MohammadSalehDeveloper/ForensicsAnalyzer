using MediatR;

namespace ForensicsAnalyzer.Application.Social.Commands;

public record UpdateSocialChatCommand(
    Guid Id,
    string Title,
    string? Description,
    string? ImageUrl,
    bool IsGroup,
    bool IsPrivate,
    DateTime? LastMessageAt
) : IRequest;
