namespace Wallet.Tracker.Domain.Services;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public interface IDbContext
{
    IQueryable<TEntity> GetQuery<TEntity>() where TEntity : class;
    void Add<TEntity>(TEntity entity) where TEntity : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
