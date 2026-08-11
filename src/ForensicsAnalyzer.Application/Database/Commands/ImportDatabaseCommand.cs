using ForensicsAnalyzer.Contracts.Database;
using MediatR;

namespace ForensicsAnalyzer.Application.Database.Commands;

public record ImportDatabaseCommand(ImportDatabaseDto Data) : IRequest;
