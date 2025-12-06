namespace ForensicsAnalyzer.Domain.Cases;

public class Case
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }
    public string OwnerUserId { get; private set; } = string.Empty;

    private Case() { } // For EF

    public Case(string name, string description, string ownerUserId)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        CreatedAt = DateTime.UtcNow;
        OwnerUserId = ownerUserId;
    }
}