using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Domain.Entities;
using MediatR;

namespace ForensicsAnalyzer.Application.Social.Commands;

public sealed class CreateSocialChatCommandHandler : IRequestHandler<CreateSocialChatCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    public CreateSocialChatCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task<Guid> Handle(CreateSocialChatCommand r, CancellationToken ct)
    {
        var entity = new SocialChat
        {
            MessengerId = r.MessengerId, Title = r.Title, Description = r.Description,
            ImageUrl = r.ImageUrl, IsGroup = r.IsGroup, IsPrivate = r.IsPrivate
        };
        _context.SocialChats.Add(entity);
        await _context.SaveChangesAsync(ct);
        return entity.Id;
    }
}
