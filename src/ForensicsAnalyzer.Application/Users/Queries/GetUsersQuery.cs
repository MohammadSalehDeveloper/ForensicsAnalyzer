using ForensicsAnalyzer.Contracts.Users;
using MediatR;

namespace ForensicsAnalyzer.Application.Users.Queries;

public sealed record GetUsersQuery() : IRequest<List<UserDto>>;
