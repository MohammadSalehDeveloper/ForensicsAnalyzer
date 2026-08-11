using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Domain.Entities;
using MediatR;

namespace ForensicsAnalyzer.Application.Locations.Commands;

public sealed class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    public CreateLocationCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task<Guid> Handle(CreateLocationCommand r, CancellationToken ct)
    {
        var entity = new Location
        {
            ArtifactId = r.ArtifactId, Latitude = r.Latitude, Longitude = r.Longitude,
            Altitude = r.Altitude, AccuracyMeters = r.AccuracyMeters, Country = r.Country,
            State = r.State, City = r.City, Street = r.Street, PostalCode = r.PostalCode,
            FullAddress = r.FullAddress, CellTowerId = r.CellTowerId,
            MobileCountryCode = r.MobileCountryCode, MobileNetworkCode = r.MobileNetworkCode,
            LocationAreaCode = r.LocationAreaCode, NetworkProvider = r.NetworkProvider,
            WifiSSID = r.WifiSSID, WifiBSSID = r.WifiBSSID, IpAddress = r.IpAddress
        };
        _context.Locations.Add(entity);
        await _context.SaveChangesAsync(ct);
        return entity.Id;
    }
}
