using MediatR;

namespace ForensicsAnalyzer.Application.Social.Commands;

public record DeleteSocialChatCommand(Guid Id) : IRequest;
