using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.ArtifactItems.Commands;

public sealed class CreateArtifactItemCommandHandler : IRequestHandler<CreateArtifactItemCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateArtifactItemCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task<Guid> Handle(CreateArtifactItemCommand request, CancellationToken cancellationToken)
    {
        var entity = new ArtifactItem
        {
            ArtifactId = request.ArtifactId,
            Type = request.Type,
            ReferenceId = request.ReferenceId
        };

        _context.ArtifactItems.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}

public sealed class DeleteArtifactItemCommandHandler : IRequestHandler<DeleteArtifactItemCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteArtifactItemCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task Handle(DeleteArtifactItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.ArtifactItems.FindAsync([request.Id], cancellationToken)
            ?? throw new KeyNotFoundException($"ArtifactItem {request.Id} not found.");

        _context.ArtifactItems.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
