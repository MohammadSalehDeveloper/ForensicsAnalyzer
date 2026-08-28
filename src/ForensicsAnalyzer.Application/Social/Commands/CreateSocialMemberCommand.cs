using MediatR;

namespace ForensicsAnalyzer.Application.Social.Commands;

public record CreateSocialMemberCommand(
    Guid ChatId,
    Guid UserId,
    string? Nickname,
    bool IsAdmin,
    bool IsMuted,
    bool IsBlocked,
    DateTime JoinedAt,
    DateTime? LastSeenAt
) : IRequest<Guid>;
