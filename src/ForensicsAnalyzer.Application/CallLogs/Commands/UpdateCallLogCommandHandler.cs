using ForensicsAnalyzer.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.CallLogs.Commands;

public sealed class UpdateCallLogCommandHandler : IRequestHandler<UpdateCallLogCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateCallLogCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task Handle(UpdateCallLogCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.CallLogs
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"CallLog {request.Id} not found.");

        entity.PhoneNumber = request.PhoneNumber;
        entity.ContactName = request.ContactName;
        entity.Type = request.Type;
        entity.CallStartTime = request.CallStartTime;
        entity.CallEndTime = request.CallEndTime;
        entity.DurationSeconds = request.DurationSeconds;
        entity.DeviceId = request.DeviceId;
        entity.Imei = request.Imei;
        entity.SimSerialNumber = request.SimSerialNumber;
        entity.CarrierName = request.CarrierName;
        entity.SourceApplication = request.SourceApplication;
        entity.IsVoip = request.IsVoip;
        entity.CellTowerId = request.CellTowerId;
        entity.Latitude = request.Latitude;
        entity.Longitude = request.Longitude;
        entity.RecordIdentifier = request.RecordIdentifier;
        entity.DatabaseSource = request.DatabaseSource;
        entity.IsRecovered = request.IsRecovered;
        entity.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
