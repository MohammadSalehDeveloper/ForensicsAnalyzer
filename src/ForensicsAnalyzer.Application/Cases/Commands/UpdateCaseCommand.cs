using MediatR;
using System;

namespace ForensicsAnalyzer.Application.Cases.Commands
{
    public class UpdateCaseCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid SourceId { get; set; }
    }
}