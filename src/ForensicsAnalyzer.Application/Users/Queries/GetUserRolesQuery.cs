using MediatR;

namespace ForensicsAnalyzer.Application.Users.Queries;

public record GetUserRolesQuery(string UserId) : IRequest<List<string>>;
