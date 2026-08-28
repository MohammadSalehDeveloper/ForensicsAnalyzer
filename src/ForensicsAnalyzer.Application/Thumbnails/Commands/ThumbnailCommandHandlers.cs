using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.Thumbnails.Commands;

public sealed class CreateThumbnailCommandHandler : IRequestHandler<CreateThumbnailCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateThumbnailCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task<Guid> Handle(CreateThumbnailCommand request, CancellationToken cancellationToken)
    {
        var entity = new Thumbnail
        {
            FileCustomId = request.FileCustomId,
            ThumbnailPath = request.ThumbnailPath,
            Width = request.Width,
            Height = request.Height,
            SizeLabel = request.SizeLabel
        };

        _context.Thumbnails.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}

public sealed class DeleteThumbnailCommandHandler : IRequestHandler<DeleteThumbnailCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteThumbnailCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task Handle(DeleteThumbnailCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Thumbnails.FindAsync([request.Id], cancellationToken)
            ?? throw new KeyNotFoundException($"Thumbnail {request.Id} not found.");

        _context.Thumbnails.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}

public sealed class UpdateThumbnailCommandHandler : IRequestHandler<UpdateThumbnailCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateThumbnailCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task Handle(UpdateThumbnailCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Thumbnails
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"Thumbnail {request.Id} not found.");

        entity.ThumbnailPath = request.ThumbnailPath;
        entity.Width = request.Width;
        entity.Height = request.Height;
        entity.SizeLabel = request.SizeLabel;
        entity.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
