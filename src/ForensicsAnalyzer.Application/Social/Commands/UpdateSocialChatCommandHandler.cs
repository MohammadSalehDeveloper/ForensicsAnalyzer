using ForensicsAnalyzer.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.Social.Commands;

public sealed class UpdateSocialChatCommandHandler : IRequestHandler<UpdateSocialChatCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateSocialChatCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task Handle(UpdateSocialChatCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.SocialChats
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"SocialChat {request.Id} not found.");

        entity.Title = request.Title;
        entity.Description = request.Description;
        entity.ImageUrl = request.ImageUrl;
        entity.IsGroup = request.IsGroup;
        entity.IsPrivate = request.IsPrivate;
        entity.LastMessageAt = request.LastMessageAt;
        entity.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
