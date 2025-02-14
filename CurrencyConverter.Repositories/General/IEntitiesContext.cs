using CurrencyConverter.Core.BaseEntities;
using Microsoft.EntityFrameworkCore;

namespace CurrencyConverter.Repositories.General
{
    public interface IEntitiesContext : IDisposable
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;
        ApplicationContext GetEntityContext();
        void SetAsAdded<TEntity>(TEntity entity) where TEntity : BaseEntity;
        void SetAsModified<TEntity>(TEntity entity) where TEntity : BaseEntity;
        void SetAsDeleted<TEntity>(TEntity entity) where TEntity : BaseEntity;
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        void BeginTransaction();
        int Commit();
        void Rollback();
        Task<int> CommitAsync();
    }
}
