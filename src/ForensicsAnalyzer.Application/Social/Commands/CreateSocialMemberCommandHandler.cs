using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Domain.Entities;
using MediatR;

namespace ForensicsAnalyzer.Application.Social.Commands;

public sealed class CreateSocialMemberCommandHandler : IRequestHandler<CreateSocialMemberCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateSocialMemberCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task<Guid> Handle(CreateSocialMemberCommand request, CancellationToken cancellationToken)
    {
        var entity = new SocialMember
        {
            ChatId = request.ChatId,
            UserId = request.UserId,
            Nickname = request.Nickname,
            IsAdmin = request.IsAdmin,
            IsMuted = request.IsMuted,
            IsBlocked = request.IsBlocked,
            JoinedAt = request.JoinedAt,
            LastSeenAt = request.LastSeenAt
        };

        _context.SocialMembers.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
