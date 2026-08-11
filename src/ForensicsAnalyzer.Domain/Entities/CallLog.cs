using ForensicsAnalyzer.Domain.Base;
using ForensicsAnalyzer.Domain.Enums;

namespace ForensicsAnalyzer.Domain.Entities;

public class CallLog : BaseEntity, ISoftDelete
{
    public Guid ArtifactId { get; set; }
    
    // Basic Call Information
    public string? PhoneNumber { get; set; }
    public string? ContactName { get; set; }
    public CallType? Type { get; set; } // Incoming, Outgoing, Missed, Rejected, VoIP
    
    // Time Information
    public DateTime? CallStartTime { get; set; }
    public DateTime? CallEndTime { get; set; }
    public int DurationSeconds { get; set; }
    
    // Device / SIM Information
    public string? DeviceId { get; set; }
    public string? Imei { get; set; }
    public string? SimSerialNumber { get; set; }
    public string? CarrierName { get; set; }
    
    // Application Information
    public string? SourceApplication { get; set; } // Phone, WhatsApp, Telegram, Signal
    public bool IsVoip { get; set; }
    
    // Location Information
    public string? CellTowerId { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    
    // File / Record Information
    public string? RecordIdentifier { get; set; }
    public string? DatabaseSource { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsRecovered { get; set; }
}