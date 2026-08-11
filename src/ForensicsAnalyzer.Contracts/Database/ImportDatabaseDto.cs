namespace ForensicsAnalyzer.Contracts.Database;

public sealed class ImportDatabaseDto
{
    public List<ImportUserDto> Users { get; set; } = new();
    public List<ImportCaseDto> Cases { get; set; } = new();
}

public sealed class ImportUserDto
{
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? FullName { get; set; }
}

public sealed class ImportCaseDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid SourceId { get; set; }
    public string OwnerEmail { get; set; } = string.Empty;
}
