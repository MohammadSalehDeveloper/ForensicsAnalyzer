using ForensicsAnalyzer.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.Sources.Commands;

public sealed class DeleteSourceCommandHandler : IRequestHandler<DeleteSourceCommand>
{
    private readonly IApplicationDbContext _context;
    public DeleteSourceCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task Handle(DeleteSourceCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Sources
            .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"Source {request.Id} not found.");

        _context.Sources.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
