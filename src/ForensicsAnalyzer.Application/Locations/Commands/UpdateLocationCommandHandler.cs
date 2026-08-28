using ForensicsAnalyzer.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.Locations.Commands;

public sealed class UpdateLocationCommandHandler : IRequestHandler<UpdateLocationCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateLocationCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Locations
            .FirstOrDefaultAsync(l => l.Id == request.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"Location {request.Id} not found.");

        entity.Latitude = request.Latitude;
        entity.Longitude = request.Longitude;
        entity.Altitude = request.Altitude;
        entity.AccuracyMeters = request.AccuracyMeters;
        entity.Country = request.Country;
        entity.State = request.State;
        entity.City = request.City;
        entity.Street = request.Street;
        entity.PostalCode = request.PostalCode;
        entity.FullAddress = request.FullAddress;
        entity.CellTowerId = request.CellTowerId;
        entity.MobileCountryCode = request.MobileCountryCode;
        entity.MobileNetworkCode = request.MobileNetworkCode;
        entity.LocationAreaCode = request.LocationAreaCode;
        entity.NetworkProvider = request.NetworkProvider;
        entity.WifiSSID = request.WifiSSID;
        entity.WifiBSSID = request.WifiBSSID;
        entity.IpAddress = request.IpAddress;
        entity.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
