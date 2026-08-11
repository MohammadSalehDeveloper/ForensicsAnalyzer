using MediatR;

namespace ForensicsAnalyzer.Application.FileCustoms.Commands;

public record UpdateFileCustomCommand(
    Guid Id,
    string FileName,
    string Extension,
    string FullPath,
    string DirectoryPath,
    string SourceImage,
    long SizeBytes,
    string MimeType,
    string? Md5Hash,
    string? Sha1Hash,
    string? Sha256Hash,
    DateTime CreatedTime,
    DateTime ModifiedTime,
    DateTime AccessedTime,
    DateTime EntryModifiedTime,
    bool IsHidden,
    bool IsSystem,
    bool IsReadOnly,
    string? Owner,
    string? Permissions,
    bool IsSuspicious,
    string? SuspiciousReason,
    string Category,
    bool IsExtracted,
    string? ExtractedFrom
) : IRequest;
