using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Domain.Entities;
using MediatR;

namespace ForensicsAnalyzer.Application.CallLogs.Commands;

public sealed class CreateCallLogCommandHandler : IRequestHandler<CreateCallLogCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    public CreateCallLogCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task<Guid> Handle(CreateCallLogCommand r, CancellationToken ct)
    {
        var entity = new CallLog
        {
            ArtifactId = r.ArtifactId, PhoneNumber = r.PhoneNumber, ContactName = r.ContactName,
            Type = r.Type, CallStartTime = r.CallStartTime, CallEndTime = r.CallEndTime,
            DurationSeconds = r.DurationSeconds, DeviceId = r.DeviceId, Imei = r.Imei,
            SimSerialNumber = r.SimSerialNumber, CarrierName = r.CarrierName,
            SourceApplication = r.SourceApplication, IsVoip = r.IsVoip,
            CellTowerId = r.CellTowerId, Latitude = r.Latitude, Longitude = r.Longitude,
            RecordIdentifier = r.RecordIdentifier, DatabaseSource = r.DatabaseSource
        };
        _context.CallLogs.Add(entity);
        await _context.SaveChangesAsync(ct);
        return entity.Id;
    }
}
