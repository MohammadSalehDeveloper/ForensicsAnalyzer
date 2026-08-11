using ForensicsAnalyzer.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.Social.Commands;

public sealed class DeleteSocialMessengerCommandHandler : IRequestHandler<DeleteSocialMessengerCommand>
{
    private readonly IApplicationDbContext _context;
    public DeleteSocialMessengerCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task Handle(DeleteSocialMessengerCommand request, CancellationToken ct)
    {
        var entity = await _context.SocialMessengers
            .FirstOrDefaultAsync(m => m.Id == request.Id, ct)
            ?? throw new KeyNotFoundException($"SocialMessenger {request.Id} not found.");
        _context.SocialMessengers.Remove(entity);
        await _context.SaveChangesAsync(ct);
    }
}
