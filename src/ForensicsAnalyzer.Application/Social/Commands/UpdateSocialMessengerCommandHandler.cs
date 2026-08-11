using ForensicsAnalyzer.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.Social.Commands;

public sealed class UpdateSocialMessengerCommandHandler : IRequestHandler<UpdateSocialMessengerCommand>
{
    private readonly IApplicationDbContext _context;
    public UpdateSocialMessengerCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task Handle(UpdateSocialMessengerCommand r, CancellationToken ct)
    {
        var entity = await _context.SocialMessengers
            .FirstOrDefaultAsync(m => m.Id == r.Id, ct)
            ?? throw new KeyNotFoundException($"SocialMessenger {r.Id} not found.");

        entity.Name = r.Name;
        entity.Description = r.Description;
        entity.ImageUrl = r.ImageUrl;
        entity.IsPublic = r.IsPublic;
        entity.IsArchived = r.IsArchived;
        entity.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync(ct);
    }
}
