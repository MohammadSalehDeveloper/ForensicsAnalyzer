using ForensicsAnalyzer.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.FileCustoms.Commands;

public sealed class DeleteFileCustomCommandHandler : IRequestHandler<DeleteFileCustomCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteFileCustomCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task Handle(DeleteFileCustomCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.FileCustoms
            .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"File {request.Id} not found.");

        entity.IsDeleted = true;
        entity.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync(cancellationToken);
    }
}
