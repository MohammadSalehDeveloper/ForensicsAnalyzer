using ForensicsAnalyzer.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.Social.Commands;

public sealed class DeleteSocialMemberCommandHandler : IRequestHandler<DeleteSocialMemberCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteSocialMemberCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task Handle(DeleteSocialMemberCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.SocialMembers
            .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"SocialMember {request.Id} not found.");

        _context.SocialMembers.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
