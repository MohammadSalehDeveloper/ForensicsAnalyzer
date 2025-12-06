using ForensicsAnalyzer.Application.Cases;
using ForensicsAnalyzer.Contracts.Cases;

namespace ForensicsAnalyzer.Infrastructure.Cases;

public sealed class CaseService : ICaseService
{
    // TEMP: dummy data; later we use SQL + EF Core
    public Task<IReadOnlyList<CaseDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var cases = new List<CaseDto>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Sample Case 001",
                Description = "Test forensics case",
                CreatedAt = DateTime.UtcNow
            }
        };

        return Task.FromResult<IReadOnlyList<CaseDto>>(cases);
    }
}
