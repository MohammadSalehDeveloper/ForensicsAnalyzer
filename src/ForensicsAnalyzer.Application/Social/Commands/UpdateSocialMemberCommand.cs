using MediatR;

namespace ForensicsAnalyzer.Application.Social.Commands;

public record UpdateSocialMemberCommand(
    Guid Id,
    string? Nickname,
    bool IsAdmin,
    bool IsMuted,
    bool IsBlocked,
    DateTime JoinedAt,
    DateTime? LastSeenAt
) : IRequest;
