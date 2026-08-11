using MediatR;

namespace ForensicsAnalyzer.Application.Social.Commands;

public record UpdateSocialMessengerCommand(
    Guid Id,
    string Name,
    string? Description,
    string? ImageUrl,
    bool IsPublic,
    bool IsArchived
) : IRequest;
