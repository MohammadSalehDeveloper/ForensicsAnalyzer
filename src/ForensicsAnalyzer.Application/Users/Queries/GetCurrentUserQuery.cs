using ForensicsAnalyzer.Contracts.Users;
using MediatR;

namespace ForensicsAnalyzer.Application.Users.Queries;

public sealed record GetCurrentUserQuery(string UserId) : IRequest<UserDto?>;
