using ForensicsAnalyzer.Application.Interfaces.Repositories;
using ForensicsAnalyzer.Domain.Entities;
using ForensicsAnalyzer.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Infrastructure.Repositories;

public class CallLogRepository : ICallLogRepository
{
    private readonly ApplicationDbContext _context;
    public CallLogRepository(ApplicationDbContext context) => _context = context;

    public async Task AddAsync(CallLog entity, CancellationToken ct)
    {
        _context.CallLogs.Add(entity);
        await _context.SaveChangesAsync(ct);
    }

    public Task<CallLog?> GetByIdAsync(Guid id, CancellationToken ct) =>
        _context.CallLogs.FirstOrDefaultAsync(c => c.Id == id, ct);

    public async Task<IReadOnlyList<CallLog>> GetByArtifactIdAsync(Guid artifactId, CancellationToken ct) =>
        await _context.CallLogs.AsNoTracking()
            .Where(c => c.ArtifactId == artifactId).ToListAsync(ct);

    public async Task UpdateAsync(CallLog entity, CancellationToken ct)
    {
        _context.CallLogs.Update(entity);
        await _context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var entity = await _context.CallLogs.FindAsync([id], ct);
        if (entity != null)
        {
            _context.CallLogs.Remove(entity);
            await _context.SaveChangesAsync(ct);
        }
    }
}
