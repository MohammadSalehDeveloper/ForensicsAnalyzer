using ForensicsAnalyzer.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.Artifacts.Commands;

public sealed class UpdateArtifactCommandHandler : IRequestHandler<UpdateArtifactCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateArtifactCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task Handle(UpdateArtifactCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Artifacts
            .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"Artifact {request.Id} not found.");

        entity.Name = request.Name;
        entity.Type = request.Type;
        entity.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
