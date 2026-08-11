using ForensicsAnalyzer.Domain.Base;
using ForensicsAnalyzer.Domain.Enums;

namespace ForensicsAnalyzer.Domain.Entities;

public class Source : BaseEntity
{
    public required string Path { get; set; }
    
    public required string Name { get; set; }

    public SourceType Type { get; set; }

    public MethodType MethodType { get; set; }

    public DeviceType DeviceType { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }

    public string? Description { get; set; }
}
