using ForensicsAnalyzer.Domain.Enums;
using MediatR;

namespace ForensicsAnalyzer.Application.CallLogs.Commands;

public record UpdateCallLogCommand(
    Guid Id,
    string? PhoneNumber,
    string? ContactName,
    CallType? Type,
    DateTime? CallStartTime,
    DateTime? CallEndTime,
    int DurationSeconds,
    string? DeviceId,
    string? Imei,
    string? SimSerialNumber,
    string? CarrierName,
    string? SourceApplication,
    bool IsVoip,
    string? CellTowerId,
    double? Latitude,
    double? Longitude,
    string? RecordIdentifier,
    string? DatabaseSource,
    bool IsRecovered
) : IRequest;
