using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Domain.Entities;
using MediatR;

namespace ForensicsAnalyzer.Application.FileCustoms.Commands;

public sealed class CreateFileCustomCommandHandler : IRequestHandler<CreateFileCustomCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateFileCustomCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task<Guid> Handle(CreateFileCustomCommand request, CancellationToken cancellationToken)
    {
        var entity = new FileCustom
        {
            FileName = request.FileName,
            Extension = request.Extension,
            FullPath = request.FullPath,
            DirectoryPath = request.DirectoryPath,
            SourceImage = request.SourceImage,
            SizeBytes = request.SizeBytes,
            MimeType = request.MimeType,
            Md5Hash = request.Md5Hash,
            Sha1Hash = request.Sha1Hash,
            Sha256Hash = request.Sha256Hash,
            CreatedTime = request.CreatedTime,
            ModifiedTime = request.ModifiedTime,
            AccessedTime = request.AccessedTime,
            EntryModifiedTime = request.EntryModifiedTime,
            IsHidden = request.IsHidden,
            IsSystem = request.IsSystem,
            IsReadOnly = request.IsReadOnly,
            Owner = request.Owner,
            Permissions = request.Permissions,
            IsSuspicious = request.IsSuspicious,
            SuspiciousReason = request.SuspiciousReason,
            Category = request.Category,
            IsExtracted = request.IsExtracted,
            ExtractedFrom = request.ExtractedFrom
        };

        _context.FileCustoms.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
