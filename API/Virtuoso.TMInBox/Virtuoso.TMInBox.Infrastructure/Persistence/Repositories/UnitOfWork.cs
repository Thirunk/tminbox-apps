using Microsoft.EntityFrameworkCore;
using Virtuoso.TMInBox.Core.Interfaces.Repository;

namespace Virtuoso.TMInBox.Infrastructure
{
    public class UnitOfWork<TDbContext> : IUnitOfWork where TDbContext : DbContext
    {
        private readonly TDbContext _dbContext;
        public UnitOfWork(TDbContext context)
        {
            _dbContext = context;
        }
        public async Task SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
           await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
