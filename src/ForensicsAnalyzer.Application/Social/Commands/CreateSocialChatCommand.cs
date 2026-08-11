using MediatR;

namespace ForensicsAnalyzer.Application.Social.Commands;

public record CreateSocialChatCommand(
    Guid MessengerId,
    string Title,
    string? Description,
    string? ImageUrl,
    bool IsGroup,
    bool IsPrivate
) : IRequest<Guid>;
