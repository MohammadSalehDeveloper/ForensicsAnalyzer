using ForensicsAnalyzer.Domain.Base;

namespace ForensicsAnalyzer.Domain.Entities;

public class LocationImage : BaseEntity
{
    public Guid FileCustomId { get; set; }
    
    public required double Latitude { get; set; }

    public required double Longitude { get; set; }

    public string? Description { get; set; }

    public bool IsPrimary { get; set; }

    public int Order { get; set; }

    // navigation
    public Location? Location { get; set; }   
}