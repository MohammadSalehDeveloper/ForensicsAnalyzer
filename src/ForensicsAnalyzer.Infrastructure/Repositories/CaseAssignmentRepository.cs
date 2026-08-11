using ForensicsAnalyzer.Application.Interfaces.Repositories;
using ForensicsAnalyzer.Domain.Entities;
using ForensicsAnalyzer.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Infrastructure.Repositories;

public class CaseAssignmentRepository : ICaseAssignmentRepository
{
    private readonly ApplicationDbContext _context;
    public CaseAssignmentRepository(ApplicationDbContext context) => _context = context;

    public async Task AddAsync(CaseAssignment entity, CancellationToken ct)
    {
        _context.CaseAssignments.Add(entity);
        await _context.SaveChangesAsync(ct);
    }

    public Task<CaseAssignment?> GetByIdAsync(Guid id, CancellationToken ct) =>
        _context.CaseAssignments.FirstOrDefaultAsync(a => a.Id == id, ct);

    public async Task<IReadOnlyList<CaseAssignment>> GetByCaseIdAsync(Guid caseId, CancellationToken ct) =>
        await _context.CaseAssignments.AsNoTracking()
            .Where(a => a.CaseId == caseId).ToListAsync(ct);

    public async Task<IReadOnlyList<CaseAssignment>> GetByUserIdAsync(string userId, CancellationToken ct) =>
        await _context.CaseAssignments.AsNoTracking()
            .Where(a => a.UserId == userId).ToListAsync(ct);

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var entity = await _context.CaseAssignments.FindAsync([id], ct);
        if (entity != null)
        {
            _context.CaseAssignments.Remove(entity);
            await _context.SaveChangesAsync(ct);
        }
    }

    public Task<bool> ExistsAsync(Guid caseId, string userId, CancellationToken ct) =>
        _context.CaseAssignments.AnyAsync(a => a.CaseId == caseId && a.UserId == userId, ct);
}
