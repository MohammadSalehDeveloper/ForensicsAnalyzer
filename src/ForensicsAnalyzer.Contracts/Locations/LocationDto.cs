namespace ForensicsAnalyzer.Contracts.Locations;

public sealed class LocationDto
{
    public Guid Id { get; set; }
    public Guid ArtifactId { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double? Altitude { get; set; }
    public double? AccuracyMeters { get; set; }
    public string? Country { get; set; }
    public string? State { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? PostalCode { get; set; }
    public string? FullAddress { get; set; }
    public string? CellTowerId { get; set; }
    public string? MobileCountryCode { get; set; }
    public string? MobileNetworkCode { get; set; }
    public string? LocationAreaCode { get; set; }
    public string? NetworkProvider { get; set; }
    public string? WifiSSID { get; set; }
    public string? WifiBSSID { get; set; }
    public string? IpAddress { get; set; }
    public DateTime CreatedAt { get; set; }
}
