using System.Linq.Expressions;
using CurrencyConverter.Core.BaseEntities;
using CurrencyConverter.Core.Data;
using CurrencyConverter.Core.DomainEntities;

namespace CurrencyConverter.RepositoryInterfaces.General
{
    public partial interface IRepository<TEntity> : IDisposable
        where TEntity : BaseEntity
    {
        Task<TEntity> FirstOrDefaultAsync();
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> LastOrDefaultAsync();
        Task<TEntity> LastOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetSingleAsync(int id);
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
        Task<bool> AnyAsync();
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
    }

}
