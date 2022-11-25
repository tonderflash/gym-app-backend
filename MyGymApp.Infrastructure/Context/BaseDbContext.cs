using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MyGymApp.Domain.Common.BaseEntity;

namespace MyGymApp.Infrastructure.Context;

public interface IDbContext
{
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
}
public abstract class BaseDbContext : DbContext, IDbContext
{
    public BaseDbContext(DbContextOptions options) : base(options)
    {
    }

    private void SetAuditEntities()
    {
        foreach (var entry in ChangeTracker.Entries<IBase>())
        {
            switch (entry.State)
            {
                case EntityState.Added:

                    entry.Entity.Deleted = false;
                    break;

                case EntityState.Deleted:

                    entry.State = EntityState.Modified;
                    entry.Entity.Deleted = true;
                    break;

                default:
                    break;
            }
        }
    }
    public override int SaveChanges()
    {
        SetAuditEntities();
        return base.SaveChanges();
    }
    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        SetAuditEntities();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        SetAuditEntities();
        return await base.SaveChangesAsync(cancellationToken);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        foreach (var type in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(IBase).IsAssignableFrom(type.ClrType))
                modelBuilder.SetSoftDeleteFilter(type.ClrType);
        }
    }
}
