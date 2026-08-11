using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.Cases.Commands.AssignCase;

public sealed class AssignCaseCommandHandler : IRequestHandler<AssignCaseCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public AssignCaseCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<Guid> Handle(AssignCaseCommand request, CancellationToken cancellationToken)
    {
        var alreadyAssigned = await _context.CaseAssignments
            .AnyAsync(a => a.CaseId == request.CaseId && a.UserId == request.UserId, cancellationToken);

        if (alreadyAssigned)
            throw new InvalidOperationException("User is already assigned to this case.");

        var assignment = new CaseAssignment
        {
            CaseId = request.CaseId,
            UserId = request.UserId,
            AssignedByUserId = _currentUser.UserId,
            AssignedAt = DateTime.UtcNow
        };

        _context.CaseAssignments.Add(assignment);
        await _context.SaveChangesAsync(cancellationToken);
        return assignment.Id;
    }
}
