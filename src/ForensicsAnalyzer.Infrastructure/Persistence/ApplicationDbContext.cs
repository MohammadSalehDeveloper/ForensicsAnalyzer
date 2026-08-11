using System.Linq.Expressions;
using System.Reflection;
using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Domain.Base;
using ForensicsAnalyzer.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    public DbSet<Case> Cases => Set<Case>();
    public DbSet<CaseAssignment> CaseAssignments => Set<CaseAssignment>();
    public DbSet<Source> Sources => Set<Source>();
    public DbSet<Artifact> Artifacts => Set<Artifact>();
    public DbSet<ArtifactItem> ArtifactItems => Set<ArtifactItem>();
    public DbSet<CallLog> CallLogs => Set<CallLog>();
    public DbSet<Contact> Contacts => Set<Contact>();
    public DbSet<FileCustom> FileCustoms => Set<FileCustom>();
    public DbSet<Location> Locations => Set<Location>();
    public DbSet<Thumbnail> Thumbnails => Set<Thumbnail>();
    public DbSet<LocationImage> LocationImages => Set<LocationImage>();
    public DbSet<SocialMessenger> SocialMessengers => Set<SocialMessenger>();
    public DbSet<SocialChat> SocialChats => Set<SocialChat>();
    public DbSet<SocialMessage> SocialMessages => Set<SocialMessage>();
    public DbSet<SocialMember> SocialMembers => Set<SocialMember>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
            {
                var method = typeof(ApplicationDbContext)
                    .GetMethod(nameof(GetSoftDeleteFilter), BindingFlags.NonPublic | BindingFlags.Static)!
                    .MakeGenericMethod(entityType.ClrType);
                modelBuilder.Entity(entityType.ClrType)
                    .HasQueryFilter((LambdaExpression)method.Invoke(null, null)!);
            }
        }
    }

    private static LambdaExpression GetSoftDeleteFilter<TEntity>()
        where TEntity : class, ISoftDelete
        => (Expression<Func<TEntity, bool>>)(e => !e.IsDeleted);

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        base.SaveChangesAsync(cancellationToken);
}
