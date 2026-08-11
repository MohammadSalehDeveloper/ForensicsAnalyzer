using ForensicsAnalyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Case> Cases { get; }
    DbSet<CaseAssignment> CaseAssignments { get; }
    DbSet<Source> Sources { get; }
    DbSet<Artifact> Artifacts { get; }
    DbSet<ArtifactItem> ArtifactItems { get; }
    DbSet<CallLog> CallLogs { get; }
    DbSet<Contact> Contacts { get; }
    DbSet<FileCustom> FileCustoms { get; }
    DbSet<Location> Locations { get; }
    DbSet<Thumbnail> Thumbnails { get; }
    DbSet<LocationImage> LocationImages { get; }
    DbSet<SocialMessenger> SocialMessengers { get; }
    DbSet<SocialChat> SocialChats { get; }
    DbSet<SocialMessage> SocialMessages { get; }
    DbSet<SocialMember> SocialMembers { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
