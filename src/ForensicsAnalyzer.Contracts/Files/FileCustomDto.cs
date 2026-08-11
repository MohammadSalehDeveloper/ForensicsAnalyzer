namespace ForensicsAnalyzer.Contracts.Files;

public sealed class FileCustomDto
{
    public Guid Id { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string Extension { get; set; } = string.Empty;
    public string FullPath { get; set; } = string.Empty;
    public string DirectoryPath { get; set; } = string.Empty;
    public string SourceImage { get; set; } = string.Empty;
    public long SizeBytes { get; set; }
    public string MimeType { get; set; } = string.Empty;
    public string? Md5Hash { get; set; }
    public string? Sha1Hash { get; set; }
    public string? Sha256Hash { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime ModifiedTime { get; set; }
    public DateTime AccessedTime { get; set; }
    public DateTime EntryModifiedTime { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsHidden { get; set; }
    public bool IsSystem { get; set; }
    public bool IsReadOnly { get; set; }
    public string? Owner { get; set; }
    public string? FilePermissions { get; set; }
    public bool IsSuspicious { get; set; }
    public string? SuspiciousReason { get; set; }
    public string Category { get; set; } = string.Empty;
    public bool IsExtracted { get; set; }
    public string? ExtractedFrom { get; set; }
    public DateTime CreatedAt { get; set; }
}
