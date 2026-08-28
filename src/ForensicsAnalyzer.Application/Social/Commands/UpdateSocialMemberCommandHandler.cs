using ForensicsAnalyzer.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.Social.Commands;

public sealed class UpdateSocialMemberCommandHandler : IRequestHandler<UpdateSocialMemberCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateSocialMemberCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task Handle(UpdateSocialMemberCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.SocialMembers
            .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"SocialMember {request.Id} not found.");

        entity.Nickname = request.Nickname;
        entity.IsAdmin = request.IsAdmin;
        entity.IsMuted = request.IsMuted;
        entity.IsBlocked = request.IsBlocked;
        entity.JoinedAt = request.JoinedAt;
        entity.LastSeenAt = request.LastSeenAt;
        entity.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
