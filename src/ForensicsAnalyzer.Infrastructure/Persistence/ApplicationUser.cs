using ForensicsAnalyzer.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace ForensicsAnalyzer.Infrastructure.Persistence;

public class ApplicationUser : IdentityUser
{
    public string? FullName { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public virtual ICollection<Case> Cases { get; set; } = new List<Case>();
    public virtual ICollection<CaseAssignment> AssignedCases { get; set; } = new List<CaseAssignment>();
}