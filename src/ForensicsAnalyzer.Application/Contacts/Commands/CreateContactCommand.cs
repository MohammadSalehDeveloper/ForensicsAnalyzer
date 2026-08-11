using MediatR;

namespace ForensicsAnalyzer.Application.Contacts.Commands;

public record CreateContactCommand(
    Guid ArtifactId,
    string? FirstName,
    string? LastName,
    string? Alias,
    string? PhoneNumber,
    string? Email,
    string? Address,
    string? Organization,
    string? JobTitle,
    string? SocialMediaHandle,
    string? Notes,
    string? Source,
    DateTime? LastContactedAt,
    bool IsSuspicious
) : IRequest<Guid>;
