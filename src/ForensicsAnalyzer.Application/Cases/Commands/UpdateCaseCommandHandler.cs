using ForensicsAnalyzer.Application.Cases.Commands;
using ForensicsAnalyzer.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.Cases.Commands;

public sealed class UpdateCaseCommandHandler : IRequestHandler<UpdateCaseCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public UpdateCaseCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task<Unit> Handle(UpdateCaseCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Cases
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken)
            ?? throw new KeyNotFoundException("Case not found.");

        entity.Name = request.Name;
        entity.Description = request.Description;
        entity.SourceId = request.SourceId;
        entity.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
