using ForensicsAnalyzer.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.Cases.Commands.UnassignCase;

public sealed class UnassignCaseCommandHandler : IRequestHandler<UnassignCaseCommand>
{
    private readonly IApplicationDbContext _context;
    public UnassignCaseCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task Handle(UnassignCaseCommand request, CancellationToken cancellationToken)
    {
        var assignment = await _context.CaseAssignments
            .FirstOrDefaultAsync(a => a.Id == request.AssignmentId, cancellationToken)
            ?? throw new KeyNotFoundException($"Assignment {request.AssignmentId} not found.");

        _context.CaseAssignments.Remove(assignment);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
