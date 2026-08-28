using ForensicsAnalyzer.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.Social.Commands;

public sealed class DeleteSocialMessageCommandHandler : IRequestHandler<DeleteSocialMessageCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteSocialMessageCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task Handle(DeleteSocialMessageCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.SocialMessages
            .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"SocialMessage {request.Id} not found.");

        entity.IsDeleted = true;
        entity.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync(cancellationToken);
    }
}
