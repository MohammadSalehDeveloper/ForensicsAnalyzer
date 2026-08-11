using ForensicsAnalyzer.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.Locations.Commands;

public sealed class DeleteLocationCommandHandler : IRequestHandler<DeleteLocationCommand>
{
    private readonly IApplicationDbContext _context;
    public DeleteLocationCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task Handle(DeleteLocationCommand request, CancellationToken ct)
    {
        var entity = await _context.Locations
            .FirstOrDefaultAsync(l => l.Id == request.Id, ct)
            ?? throw new KeyNotFoundException($"Location {request.Id} not found.");
        _context.Locations.Remove(entity);
        await _context.SaveChangesAsync(ct);
    }
}
