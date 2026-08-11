using ForensicsAnalyzer.Domain.Base;

namespace ForensicsAnalyzer.Domain.Entities;

public class Location : BaseEntity
{
    public Guid ArtifactId { get; set; }
    
    // Geographic Coordinates
    public required double Latitude { get; set; }
    public required double Longitude { get; set; }
    public double? Altitude { get; set; }
    public double? AccuracyMeters { get; set; }

    // Address Information
    public string? Country { get; set; }
    public string? State { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? PostalCode { get; set; }
    public string? FullAddress { get; set; }

    // Network / Cell Tower Information
    public string? CellTowerId { get; set; }
    public string? MobileCountryCode { get; set; } // MCC
    public string? MobileNetworkCode { get; set; } // MNC
    public string? LocationAreaCode { get; set; } // LAC
    public string? NetworkProvider { get; set; }

    // WiFi / IP Metadata
    public string? WifiSSID { get; set; }
    public string? WifiBSSID { get; set; }
    public string? IpAddress { get; set; }
}