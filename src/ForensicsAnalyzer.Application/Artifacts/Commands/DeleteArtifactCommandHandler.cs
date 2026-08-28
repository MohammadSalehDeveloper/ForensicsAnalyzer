using ForensicsAnalyzer.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.Artifacts.Commands;

public sealed class DeleteArtifactCommandHandler : IRequestHandler<DeleteArtifactCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteArtifactCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task Handle(DeleteArtifactCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Artifacts
            .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"Artifact {request.Id} not found.");

        _context.Artifacts.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
