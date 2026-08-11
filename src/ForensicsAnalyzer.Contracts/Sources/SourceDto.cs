using ForensicsAnalyzer.Domain.Enums;

namespace ForensicsAnalyzer.Contracts.Sources;

public sealed class SourceDto
{
    public Guid Id { get; set; }
    public string Path { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public SourceType Type { get; set; }
    public MethodType MethodType { get; set; }
    public DeviceType DeviceType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
}
