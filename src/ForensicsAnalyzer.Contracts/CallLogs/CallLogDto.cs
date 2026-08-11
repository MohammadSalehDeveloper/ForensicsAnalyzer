using ForensicsAnalyzer.Domain.Enums;

namespace ForensicsAnalyzer.Contracts.CallLogs;

public sealed class CallLogDto
{
    public Guid Id { get; set; }
    public Guid ArtifactId { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ContactName { get; set; }
    public CallType? Type { get; set; }
    public DateTime? CallStartTime { get; set; }
    public DateTime? CallEndTime { get; set; }
    public int DurationSeconds { get; set; }
    public string? DeviceId { get; set; }
    public string? Imei { get; set; }
    public string? SimSerialNumber { get; set; }
    public string? CarrierName { get; set; }
    public string? SourceApplication { get; set; }
    public bool IsVoip { get; set; }
    public string? CellTowerId { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public string? RecordIdentifier { get; set; }
    public string? DatabaseSource { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsRecovered { get; set; }
    public DateTime CreatedAt { get; set; }
}
