using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.LocationImages.Commands;

public sealed class CreateLocationImageCommandHandler : IRequestHandler<CreateLocationImageCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateLocationImageCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task<Guid> Handle(CreateLocationImageCommand request, CancellationToken cancellationToken)
    {
        var entity = new LocationImage
        {
            FileCustomId = request.FileCustomId,
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            Description = request.Description,
            IsPrimary = request.IsPrimary,
            Order = request.Order
        };

        _context.LocationImages.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}

public sealed class UpdateLocationImageCommandHandler : IRequestHandler<UpdateLocationImageCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateLocationImageCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task Handle(UpdateLocationImageCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.LocationImages
            .FirstOrDefaultAsync(li => li.Id == request.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"LocationImage {request.Id} not found.");

        entity.Latitude = request.Latitude;
        entity.Longitude = request.Longitude;
        entity.Description = request.Description;
        entity.IsPrimary = request.IsPrimary;
        entity.Order = request.Order;
        entity.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
    }
}

public sealed class DeleteLocationImageCommandHandler : IRequestHandler<DeleteLocationImageCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteLocationImageCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task Handle(DeleteLocationImageCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.LocationImages.FindAsync([request.Id], cancellationToken)
            ?? throw new KeyNotFoundException($"LocationImage {request.Id} not found.");

        _context.LocationImages.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
