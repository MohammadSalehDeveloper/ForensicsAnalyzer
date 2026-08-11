using ForensicsAnalyzer.Application.Cases.Commands;
using ForensicsAnalyzer.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.Cases.Commands;

public sealed class DeleteCaseCommandHandler : IRequestHandler<DeleteCaseCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public DeleteCaseCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task<Unit> Handle(DeleteCaseCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Cases
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken)
            ?? throw new KeyNotFoundException("Case not found.");

        entity.IsDeleted = true;
        entity.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
