using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Domain.Entities;
using MediatR;

namespace ForensicsAnalyzer.Application.Social.Commands;

public sealed class CreateSocialMessengerCommandHandler : IRequestHandler<CreateSocialMessengerCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    public CreateSocialMessengerCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task<Guid> Handle(CreateSocialMessengerCommand r, CancellationToken ct)
    {
        var entity = new SocialMessenger
        {
            Name = r.Name, Description = r.Description, ImageUrl = r.ImageUrl, IsPublic = r.IsPublic
        };
        _context.SocialMessengers.Add(entity);
        await _context.SaveChangesAsync(ct);
        return entity.Id;
    }
}
