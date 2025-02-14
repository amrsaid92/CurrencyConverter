using System.Collections;
using CurrencyConverter.Core.BaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace CurrencyConverter.Repositories.General
{
    public class EntityContext : IEntitiesContext
    {
        private readonly ApplicationContext _objectContext;
        private IDbContextTransaction _transaction;
        private static readonly object Lock = new object();
        private static bool _databaseInitialized;
        public EntityContext()
        {
            _objectContext = new ApplicationContext();
            if (_databaseInitialized)
            {
                return;
            }
            lock (Lock)
            {
                if (!_databaseInitialized)
                {
                    _databaseInitialized = true;
                }
            }
        }

        public ApplicationContext GetEntityContext()
        {
            return
                _objectContext;
        }

        public void SetAsAdded<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            UpdateEntityState(entity, EntityState.Added);
            TrackGraph(entity);
        }

        public void SetAsModified<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            var entry = UpdateEntityState(entity, EntityState.Modified);
            TrackGraph(entity);
        }

        public void SetAsDeleted<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            UpdateEntityState(entity, EntityState.Deleted);
        }

        public void BeginTransaction() => _transaction = _objectContext.Database.CurrentTransaction ?? _objectContext.Database.BeginTransaction();

        public int Commit()
        {
            var saveChanges = _objectContext.SaveChanges();
            _transaction.Commit();
            return saveChanges;
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        public Task<int> CommitAsync()
        {
            var saveChangesAsync = _objectContext.SaveChangesAsync();
            _transaction.Commit();
            return saveChangesAsync;
        }

        private EntityEntry UpdateEntityState<TEntity>(TEntity entity, EntityState entityState) where TEntity : BaseEntity
        {
            var dbEntityEntry = GetDbEntityEntrySafely(entity);
            dbEntityEntry.State = entityState;
            return dbEntityEntry;
        }
        private void TrackGraph(object objEntity)
        {
            _objectContext.ChangeTracker.TrackGraph(objEntity, node =>
            {
                var propertyEntry = node.Entry.Property("Id");
                var keyValue = (int)propertyEntry.CurrentValue;

                if (keyValue == 0)
                {
                    node.Entry.State = EntityState.Added;
                }
                else if (keyValue < 0)
                {
                    propertyEntry.CurrentValue = -keyValue;
                    node.Entry.State = EntityState.Deleted;
                }
                else
                {
                    if (node.GetType().GetProperty("CreatedBy") != null)
                        _objectContext.Entry(node).Property("CreatedBy").IsModified = false;
                    if (node.GetType().GetProperty("CreatedAt") != null)
                        _objectContext.Entry(node).Property("CreatedAt").IsModified = false;
                    node.Entry.State = EntityState.Modified;
                }
            });
        }

        private EntityEntry GetDbEntityEntrySafely<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            var entry = _objectContext.ChangeTracker.Entries().FirstOrDefault(e => e.Entity.GetType() == entity.GetType() &&
            e.Entity.GetType().GetProperty("Id").GetValue(e.Entity)?.ToString() == entity.Id.ToString());
            if (entry == null)
                //_objectContext.Entry(entry.Entity).State = EntityState.Detached;
                entry = _objectContext.Set<TEntity>().Attach(entity);
            return entry;
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                _objectContext.Dispose();
            }
        }

        public void Dispose()
        {
            _objectContext.Dispose();
        }

        public DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return _objectContext.Set<TEntity>();
        }

        public int SaveChanges()
        {
            return _objectContext.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return _objectContext.SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _objectContext.SaveChangesAsync(cancellationToken);
        }
    }
}
