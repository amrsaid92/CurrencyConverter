using System.Linq.Expressions;
using CurrencyConverter.Core.BaseEntities;
using CurrencyConverter.Core.Data;
using CurrencyConverter.Core.DomainEntities;

namespace CurrencyConverter.RepositoryInterfaces.General
{
    public partial interface IRepository<TEntity> : IDisposable
        where TEntity : BaseEntity
    {
        IRepository<TEntity> AsTracking();
        IRepository<TEntity> AsNoTracking();
        IRepository<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties);
        TEntity FirstOrDefault();
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        TEntity LastOrDefault();
        TEntity LastOrDefault(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, OrderBy order);
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, int>> keySelector, OrderBy order);
        PaginatedList<TEntity> GetAll(int pageIndex, int pageSize);
        PaginatedList<TEntity> GetAll(int pageIndex, int pageSize, Expression<Func<TEntity, int>> keySelector, OrderBy orderBy = OrderBy.Ascending);
        PaginatedList<TEntity> GetAll(int pageIndex, int pageSize, Expression<Func<TEntity, int>> keySelector, Expression<Func<TEntity, bool>> predicate, OrderBy orderBy);
        PaginatedList<TEntity> GetAll(int pageIndex, int pageSize, string keySelector, Expression<Func<TEntity, bool>> predicate, OrderBy orderBy);
        TEntity GetSingle(int id);
        int Count();
        int Count(Expression<Func<TEntity, bool>> predicate);
        bool Any();
        bool Any(Expression<Func<TEntity, bool>> predicate);
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }

}
