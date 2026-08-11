namespace ForensicsAnalyzer.Contracts.Thumbnails;

public sealed class ThumbnailDto
{
    public Guid Id { get; set; }
    public Guid FileCustomId { get; set; }
    public string ThumbnailPath { get; set; } = string.Empty;
    public int Width { get; set; }
    public int Height { get; set; }
    public string? SizeLabel { get; set; }
    public DateTime CreatedAt { get; set; }
}
