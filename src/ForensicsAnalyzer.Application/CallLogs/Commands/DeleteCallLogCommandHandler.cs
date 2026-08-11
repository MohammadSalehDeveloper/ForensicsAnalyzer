using ForensicsAnalyzer.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.CallLogs.Commands;

public sealed class DeleteCallLogCommandHandler : IRequestHandler<DeleteCallLogCommand>
{
    private readonly IApplicationDbContext _context;
    public DeleteCallLogCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task Handle(DeleteCallLogCommand request, CancellationToken ct)
    {
        var entity = await _context.CallLogs
            .FirstOrDefaultAsync(c => c.Id == request.Id, ct)
            ?? throw new KeyNotFoundException($"CallLog {request.Id} not found.");
        entity.IsDeleted = true;
        await _context.SaveChangesAsync(ct);
    }
}
