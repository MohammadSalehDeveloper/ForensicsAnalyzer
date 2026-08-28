using ForensicsAnalyzer.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.Social.Commands;

public sealed class UpdateSocialMessageCommandHandler : IRequestHandler<UpdateSocialMessageCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateSocialMessageCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task Handle(UpdateSocialMessageCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.SocialMessages
            .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"SocialMessage {request.Id} not found.");

        entity.Content = request.Content;
        entity.AttachmentUrl = request.AttachmentUrl;
        entity.AttachmentType = request.AttachmentType;
        entity.IsEdited = request.IsEdited;
        entity.SentAt = request.SentAt;
        entity.EditedAt = request.EditedAt;
        entity.ReplyToMessageId = request.ReplyToMessageId;
        entity.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
