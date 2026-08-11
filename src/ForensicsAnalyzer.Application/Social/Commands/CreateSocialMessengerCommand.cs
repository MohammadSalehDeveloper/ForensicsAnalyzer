using MediatR;

namespace ForensicsAnalyzer.Application.Social.Commands;

public record CreateSocialMessengerCommand(
    string Name,
    string? Description,
    string? ImageUrl,
    bool IsPublic
) : IRequest<Guid>;
