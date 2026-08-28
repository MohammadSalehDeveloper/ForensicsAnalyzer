using ForensicsAnalyzer.Contracts.Users;
using MediatR;

namespace ForensicsAnalyzer.Application.Users.Queries;

public record GetUserByIdQuery(string Id) : IRequest<UserDto?>;
