using ForensicsAnalyzer.Domain.Cases;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Case> Cases { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}