using ForensicsAnalyzer.Application.Interfaces.Repositories;
using ForensicsAnalyzer.Domain.Entities;
using ForensicsAnalyzer.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Infrastructure.Repositories;

public class CaseRepository : ICaseRepository
{
    private readonly ApplicationDbContext _context;

    public CaseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Case entity, CancellationToken ct)
    {
        _context.Cases.Add(entity);
        await _context.SaveChangesAsync(ct);
    }

    public Task<Case?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var caseFound = _context.Cases.FirstOrDefault(c => c.Id == id);

        if (caseFound is null)
        {
            return Task.FromResult<Case?>(null);
        }

        return Task.FromResult(caseFound)!;
    }

    public async Task<IReadOnlyList<Case>> GetAllAsync(CancellationToken ct)
    {
        return await _context.Cases.AsNoTracking().ToListAsync(cancellationToken: ct);
    }
}