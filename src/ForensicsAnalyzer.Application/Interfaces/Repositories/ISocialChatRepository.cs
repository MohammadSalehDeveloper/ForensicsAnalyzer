using ForensicsAnalyzer.Domain.Entities;

namespace ForensicsAnalyzer.Application.Interfaces.Repositories;

public interface ISocialChatRepository
{
    Task AddAsync(SocialChat entity, CancellationToken ct);
    Task<SocialChat?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<IReadOnlyList<SocialChat>> GetByMessengerIdAsync(Guid messengerId, CancellationToken ct);
    Task UpdateAsync(SocialChat entity, CancellationToken ct);
    Task DeleteAsync(Guid id, CancellationToken ct);
}
