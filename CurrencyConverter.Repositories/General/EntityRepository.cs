using System.Collections;
using System.Linq.Expressions;
using CurrencyConverter.Core.BaseEntities;
using CurrencyConverter.Core.Data;
using CurrencyConverter.Core.DomainEntities;
using CurrencyConverter.Core.Extensions;
using CurrencyConverter.RepositoryInterfaces.General;
using Microsoft.EntityFrameworkCore;

namespace CurrencyConverter.Repositories.General
{
    public class EntityRepository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        internal readonly IEntitiesContext _context;
        internal readonly DbSet<TEntity> _dbSet;
        private bool _disposed;
        private bool _tracking = false;
        private Expression<Func<TEntity, object>>[] _includeProperties;
        public EntityRepository(IEntitiesContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }
        public bool Any()
        {
            return _dbSet.Any();
        }

        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Any(predicate);
        }

        public Task<bool> AnyAsync()
        {
            return _dbSet.AnyAsync();
        }

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.AnyAsync(predicate);
        }

        public IRepository<TEntity> AsNoTracking()
        {
            _tracking = false;
            return this;
        }

        public IRepository<TEntity> AsTracking()
        {
            _tracking = true;
            return this;
        }

        public int Count()
        {
            return _dbSet.Count();
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Count(predicate);
        }

        public Task<int> CountAsync()
        {
            return _dbSet.CountAsync();
        }

        public Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.CountAsync(predicate);
        }

        public void Delete(TEntity entity)
        {
            _context.SetAsDeleted(entity);
        }

        public TEntity FirstOrDefault()
        {
            return GetDbSet().FirstOrDefault();
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return GetDbSet().FirstOrDefault(predicate);
        }

        public Task<TEntity> FirstOrDefaultAsync()
        {
            return GetDbSet().FirstOrDefaultAsync();
        }

        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return GetDbSet().FirstOrDefaultAsync(predicate);
        }

        public IQueryable<TEntity> GetAll()
        {
            return GetDbSet();
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return GetDbSet().Where(predicate);
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, OrderBy order)
        {
            return FilterQuery(x => x.Id, predicate, order);
        }
        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, int>> keySelector, OrderBy order)
        {
            return FilterQuery(keySelector, predicate, order);
        }

        public PaginatedList<TEntity> GetAll(int pageIndex, int pageSize)
        {
            return GetAll(pageIndex, pageSize, x => x.Id);
        }

        public PaginatedList<TEntity> GetAll(int pageIndex, int pageSize, Expression<Func<TEntity, int>> keySelector, OrderBy orderBy = OrderBy.Ascending)
        {
            return GetAll(pageIndex, pageSize, keySelector, null, orderBy);
        }

        public PaginatedList<TEntity> GetAll(int pageIndex, int pageSize, Expression<Func<TEntity, int>> keySelector, Expression<Func<TEntity, bool>> predicate, OrderBy orderBy)
        {
            var entities = FilterQuery(keySelector, predicate, orderBy);
            var total = entities.Count();
            entities = entities.Paginate(pageIndex, pageSize);
            return entities.ToPaginatedList(pageIndex, pageSize, total);
        }

        public PaginatedList<TEntity> GetAll(int pageIndex, int pageSize, string keySelector, Expression<Func<TEntity, bool>> predicate, OrderBy orderBy)
        {
            var entities = FilterQuery(keySelector, predicate, orderBy);
            var total = entities.Count();
            entities = entities.Paginate(pageIndex, pageSize);
            return entities.ToPaginatedList(pageIndex, pageSize, total);
        }

        public TEntity GetSingle(int id)
        {
            return FirstOrDefault(x => x.Id == id);
        }

        public Task<TEntity> GetSingleAsync(int id)
        {
            return FirstOrDefaultAsync(x => x.Id == id);
        }

        public IRepository<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            _includeProperties = includeProperties;
            return this;
        }

        public void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
            _context.SetAsAdded(entity);
        }

        public TEntity LastOrDefault()
        {
            return GetDbSet().OrderBy(itm => itm.Id).LastOrDefault();
        }

        public TEntity LastOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return GetDbSet().OrderBy(itm => itm.Id).LastOrDefault(predicate);
        }

        public Task<TEntity> LastOrDefaultAsync()
        {
            return GetDbSet().OrderBy(itm => itm.Id).LastOrDefaultAsync();
        }

        public Task<TEntity> LastOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return GetDbSet().LastOrDefaultAsync(predicate);
        }

        public void Update(TEntity entity)
        {
            _context.SetAsModified(entity);
        }

        private IQueryable<TEntity> FilterQuery(Expression<Func<TEntity, int>> keySelector, Expression<Func<TEntity, bool>> predicate, OrderBy orderBy)
        {
            var entities = GetDbSet();
            entities = (predicate != null) ? entities.Where(predicate) : entities;
            entities = (orderBy == OrderBy.Ascending)
                ? entities.OrderBy(keySelector)
                : entities.OrderByDescending(keySelector);
            return entities;
        }
        private IQueryable<TEntity> FilterQuery(string keySelector, Expression<Func<TEntity, bool>> predicate, OrderBy orderBy)
        {
            var entities = GetDbSet();
            entities = (predicate != null) ? entities.Where(predicate) : entities;
            if (!string.IsNullOrWhiteSpace(keySelector))
                entities = (orderBy == OrderBy.Ascending)
                    ? entities.OrderBy(keySelector)
                    : entities.OrderByDescending(keySelector);
            else
                entities = (orderBy == OrderBy.Ascending)
                    ? entities.OrderBy(x => x.Id)
                    : entities.OrderByDescending(x => x.Id);
            return entities;
        }
        private IQueryable<TEntity> GetDbSet()
        {
            IQueryable<TEntity> entities = _dbSet;
            if (_includeProperties != null)
            {
                foreach (var prop in _includeProperties)
                {
                    entities = entities.Include(prop);
                }
            }

            if (_tracking)
                return entities;
            else
                return entities.AsNoTracking();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }

    }
}
