using ForensicsAnalyzer.Contracts.Files;
using MediatR;

namespace ForensicsAnalyzer.Application.FileCustoms.Queries;

public record GetFileCustomsQuery : IRequest<List<FileCustomDto>>;

public record GetFileCustomByIdQuery(Guid Id) : IRequest<FileCustomDto?>;
