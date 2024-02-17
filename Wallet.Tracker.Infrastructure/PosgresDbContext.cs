namespace Wallet.Tracker.Infrastructure;

using Wallet.Tracker.Domain.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection;
using System.Threading;

public class PosgresDbContext : DbContext, IDbContext
{
    public PosgresDbContext() : base()
    {

    }

    public PosgresDbContext(DbContextOptions<PosgresDbContext> options) : base(options)
    {

    }

    public IQueryable<TEntity> GetQuery<TEntity>()
        where TEntity : class
        => Set<TEntity>();

    void IDbContext.Add<TEntity>(TEntity entity)
    {
        Add(entity);
    }

    Task<int> IDbContext.SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
