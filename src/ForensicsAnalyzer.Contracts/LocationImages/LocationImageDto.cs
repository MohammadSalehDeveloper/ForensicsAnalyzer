namespace ForensicsAnalyzer.Contracts.LocationImages;

public sealed class LocationImageDto
{
    public Guid Id { get; set; }
    public Guid FileCustomId { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? Description { get; set; }
    public bool IsPrimary { get; set; }
    public int Order { get; set; }
    public DateTime CreatedAt { get; set; }
}
