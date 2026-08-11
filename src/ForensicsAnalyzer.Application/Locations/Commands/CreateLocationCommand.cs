using MediatR;

namespace ForensicsAnalyzer.Application.Locations.Commands;

public record CreateLocationCommand(
    Guid ArtifactId,
    double Latitude,
    double Longitude,
    double? Altitude,
    double? AccuracyMeters,
    string? Country,
    string? State,
    string? City,
    string? Street,
    string? PostalCode,
    string? FullAddress,
    string? CellTowerId,
    string? MobileCountryCode,
    string? MobileNetworkCode,
    string? LocationAreaCode,
    string? NetworkProvider,
    string? WifiSSID,
    string? WifiBSSID,
    string? IpAddress
) : IRequest<Guid>;
