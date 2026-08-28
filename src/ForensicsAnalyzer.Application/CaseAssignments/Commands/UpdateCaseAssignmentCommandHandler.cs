using ForensicsAnalyzer.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.CaseAssignments.Commands;

public sealed class UpdateCaseAssignmentCommandHandler : IRequestHandler<UpdateCaseAssignmentCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public UpdateCaseAssignmentCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task Handle(UpdateCaseAssignmentCommand request, CancellationToken cancellationToken)
    {
        var assignment = await _context.CaseAssignments
            .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"CaseAssignment {request.Id} not found.");

        if (assignment.UserId != request.UserId)
        {
            var duplicate = await _context.CaseAssignments
                .AnyAsync(a => a.CaseId == assignment.CaseId && a.UserId == request.UserId && a.Id != request.Id, cancellationToken);

            if (duplicate)
                throw new InvalidOperationException("User is already assigned to this case.");

            assignment.UserId = request.UserId;
        }

        assignment.AssignedByUserId = _currentUser.UserId;
        assignment.AssignedAt = DateTime.UtcNow;
        assignment.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
