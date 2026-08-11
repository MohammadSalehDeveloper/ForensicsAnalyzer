using ForensicsAnalyzer.Domain.Entities;

namespace ForensicsAnalyzer.Application.Interfaces.Repositories;

public interface ICaseAssignmentRepository
{
    Task AddAsync(CaseAssignment entity, CancellationToken ct);
    Task<CaseAssignment?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<IReadOnlyList<CaseAssignment>> GetByCaseIdAsync(Guid caseId, CancellationToken ct);
    Task<IReadOnlyList<CaseAssignment>> GetByUserIdAsync(string userId, CancellationToken ct);
    Task DeleteAsync(Guid id, CancellationToken ct);
    Task<bool> ExistsAsync(Guid caseId, string userId, CancellationToken ct);
}
