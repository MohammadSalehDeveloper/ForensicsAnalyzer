using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Domain.Entities;
using MediatR;

namespace ForensicsAnalyzer.Application.Artifacts.Commands;

public sealed class CreateArtifactCommandHandler : IRequestHandler<CreateArtifactCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateArtifactCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task<Guid> Handle(CreateArtifactCommand request, CancellationToken cancellationToken)
    {
        var entity = new Artifact
        {
            CaseId = request.CaseId,
            Name = request.Name,
            Type = request.Type
        };

        _context.Artifacts.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
