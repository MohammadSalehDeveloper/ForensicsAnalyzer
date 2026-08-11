using MediatR;
using System;

namespace ForensicsAnalyzer.Application.Cases.Commands
{
    public class DeleteCaseCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}