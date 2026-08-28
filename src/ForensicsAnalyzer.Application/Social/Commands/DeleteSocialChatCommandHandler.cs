using ForensicsAnalyzer.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.Social.Commands;

public sealed class DeleteSocialChatCommandHandler : IRequestHandler<DeleteSocialChatCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteSocialChatCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task Handle(DeleteSocialChatCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.SocialChats
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"SocialChat {request.Id} not found.");

        _context.SocialChats.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
