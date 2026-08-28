using MediatR;

namespace ForensicsAnalyzer.Application.Users.Queries;

public record GetRolesQuery : IRequest<List<string>>;
