using ForensicsAnalyzer.Domain.Base;

namespace ForensicsAnalyzer.Domain.Entities;

public class Thumbnail : BaseEntity
{
    public Guid FileCustomId { get; set; }

    public required string ThumbnailPath { get; set; }

    public int Width { get; set; }

    public int Height { get; set; }

    public string? SizeLabel { get; set; }
}