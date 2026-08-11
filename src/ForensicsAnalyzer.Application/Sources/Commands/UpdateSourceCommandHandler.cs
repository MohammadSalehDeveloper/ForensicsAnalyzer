using ForensicsAnalyzer.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.Sources.Commands;

public sealed class UpdateSourceCommandHandler : IRequestHandler<UpdateSourceCommand>
{
    private readonly IApplicationDbContext _context;
    public UpdateSourceCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task Handle(UpdateSourceCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Sources
            .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"Source {request.Id} not found.");

        entity.Name = request.Name;
        entity.Path = request.Path;
        entity.Type = request.Type;
        entity.MethodType = request.MethodType;
        entity.DeviceType = request.DeviceType;
        entity.StartDate = request.StartDate;
        entity.EndDate = request.EndDate;
        entity.Description = request.Description;
        entity.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
