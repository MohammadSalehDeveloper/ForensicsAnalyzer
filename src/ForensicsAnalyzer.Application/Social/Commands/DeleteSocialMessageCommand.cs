using MediatR;

namespace ForensicsAnalyzer.Application.Social.Commands;

public record DeleteSocialMessageCommand(Guid Id) : IRequest;
