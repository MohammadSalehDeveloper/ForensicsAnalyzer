using MediatR;

namespace ForensicsAnalyzer.Application.Social.Commands;

public record DeleteSocialMessengerCommand(Guid Id) : IRequest;
