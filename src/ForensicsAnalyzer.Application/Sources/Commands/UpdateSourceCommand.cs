using ForensicsAnalyzer.Domain.Enums;
using MediatR;

namespace ForensicsAnalyzer.Application.Sources.Commands;

public record UpdateSourceCommand(
    Guid Id,
    string Name,
    string Path,
    SourceType Type,
    MethodType MethodType,
    DeviceType DeviceType,
    DateTime StartDate,
    DateTime EndDate,
    string? Description
) : IRequest;
