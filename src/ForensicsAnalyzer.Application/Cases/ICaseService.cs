using ForensicsAnalyzer.Contracts.Cases;

namespace ForensicsAnalyzer.Application.Cases;

public interface ICaseService
{
    Task<IReadOnlyList<CaseDto>> GetAllAsync(CancellationToken cancellationToken = default);
}