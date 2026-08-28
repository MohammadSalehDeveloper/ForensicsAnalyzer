using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Domain.Entities;
using MediatR;

namespace ForensicsAnalyzer.Application.Social.Commands;

public sealed class CreateSocialMessageCommandHandler : IRequestHandler<CreateSocialMessageCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateSocialMessageCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task<Guid> Handle(CreateSocialMessageCommand request, CancellationToken cancellationToken)
    {
        var entity = new SocialMessage
        {
            ChatId = request.ChatId,
            SenderId = request.SenderId,
            Content = request.Content,
            AttachmentUrl = request.AttachmentUrl,
            AttachmentType = request.AttachmentType,
            SentAt = request.SentAt,
            ReplyToMessageId = request.ReplyToMessageId
        };

        _context.SocialMessages.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
