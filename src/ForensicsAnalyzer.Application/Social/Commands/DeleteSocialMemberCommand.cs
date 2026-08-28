using MediatR;

namespace ForensicsAnalyzer.Application.Social.Commands;

public record DeleteSocialMemberCommand(Guid Id) : IRequest;
