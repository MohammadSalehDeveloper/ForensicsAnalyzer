using ForensicsAnalyzer.Domain.Base;

namespace ForensicsAnalyzer.Domain.Entities;

public class FileCustom : BaseEntity, ISoftDelete
{
    // Identification
    public required string FileName { get; set; }
    public required string Extension { get; set; }

    // Path & Source
    public required string FullPath { get; set; }
    public required string DirectoryPath { get; set; }
    public required string SourceImage { get; set; } // Disk image or evidence source

    // Size & Type
    public required long SizeBytes { get; set; }
    public required string MimeType { get; set; }
    
    // Hashes (For integrity verification)
    public string? Md5Hash { get; set; }
    public string? Sha1Hash { get; set; }
    public string? Sha256Hash { get; set; }

    // File System Metadata
    public DateTime CreatedTime { get; set; }
    public DateTime ModifiedTime { get; set; }
    public DateTime AccessedTime { get; set; }
    public DateTime EntryModifiedTime { get; set; } // MFT change time (NTFS)

    // File System Attributes
    public bool IsDeleted { get; set; }
    public bool IsHidden { get; set; }
    public bool IsSystem { get; set; }
    public bool IsReadOnly { get; set; }

    // Ownership / Security
    public string? Owner { get; set; }
    public string? Permissions { get; set; }

    // Forensic Analysis
    public bool IsSuspicious { get; set; }
    public string? SuspiciousReason { get; set; }
    public required string Category { get; set; } // Document, Executable, Image, etc.

    // Extraction Info
    public bool IsExtracted { get; set; }
    public string? ExtractedFrom { get; set; }
}