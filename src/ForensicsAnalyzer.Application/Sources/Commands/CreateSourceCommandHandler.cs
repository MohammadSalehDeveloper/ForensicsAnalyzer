using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Domain.Entities;
using MediatR;

namespace ForensicsAnalyzer.Application.Sources.Commands;

public sealed class CreateSourceCommandHandler : IRequestHandler<CreateSourceCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    public CreateSourceCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task<Guid> Handle(CreateSourceCommand request, CancellationToken cancellationToken)
    {
        var entity = new Source
        {
            Name = request.Name,
            Path = request.Path,
            Type = request.Type,
            MethodType = request.MethodType,
            DeviceType = request.DeviceType,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Description = request.Description
        };

        _context.Sources.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
