using ForensicsAnalyzer.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.FileCustoms.Commands;

public sealed class UpdateFileCustomCommandHandler : IRequestHandler<UpdateFileCustomCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateFileCustomCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task Handle(UpdateFileCustomCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.FileCustoms
            .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"File {request.Id} not found.");

        entity.FileName = request.FileName;
        entity.Extension = request.Extension;
        entity.FullPath = request.FullPath;
        entity.DirectoryPath = request.DirectoryPath;
        entity.SourceImage = request.SourceImage;
        entity.SizeBytes = request.SizeBytes;
        entity.MimeType = request.MimeType;
        entity.Md5Hash = request.Md5Hash;
        entity.Sha1Hash = request.Sha1Hash;
        entity.Sha256Hash = request.Sha256Hash;
        entity.CreatedTime = request.CreatedTime;
        entity.ModifiedTime = request.ModifiedTime;
        entity.AccessedTime = request.AccessedTime;
        entity.EntryModifiedTime = request.EntryModifiedTime;
        entity.IsHidden = request.IsHidden;
        entity.IsSystem = request.IsSystem;
        entity.IsReadOnly = request.IsReadOnly;
        entity.Owner = request.Owner;
        entity.Permissions = request.Permissions;
        entity.IsSuspicious = request.IsSuspicious;
        entity.SuspiciousReason = request.SuspiciousReason;
        entity.Category = request.Category;
        entity.IsExtracted = request.IsExtracted;
        entity.ExtractedFrom = request.ExtractedFrom;
        entity.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
