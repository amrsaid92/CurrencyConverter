using CurrencyConverter.Core.BaseEntities;
using CurrencyConverter.Core.Data;
using CurrencyConverter.Core.DomainEntities;
using System.Linq.Expressions;

namespace CurrencyConverter.ServiceInterfaces.General
{
    public interface IService<TEntityDto>
       where TEntityDto : BaseEntityDto
    {
        IService<TEntityDto> AsTracking();
        IService<TEntityDto> AsNoTracking();
        IService<TEntityDto> Include(params Expression<Func<TEntityDto, object>>[] includeProperties);
        ResponseResult<TEntityDto> FirstOrDefault();
        ResponseResult<TEntityDto> FirstOrDefault(Expression<Func<TEntityDto, bool>> predicate);
        ResponseResult<TEntityDto> LastOrDefault();
        ResponseResult<TEntityDto> LastOrDefault(Expression<Func<TEntityDto, bool>> predicate);
        ResponseResult<List<TEntityDto>> GetAll();
        ResponseResult<List<TEntityDto>> GetAll(Expression<Func<TEntityDto, bool>> predicate);
        ResponseResult<List<TEntityDto>> GetAll(Expression<Func<TEntityDto, bool>> predicate, OrderBy order);
        ResponseResult<List<TEntityDto>> GetAll(Expression<Func<TEntityDto, bool>> predicate, Expression<Func<TEntityDto, int>> keySelector, OrderBy order);
        ResponseResult<PaginatedList<TEntityDto>> GetAll(int pageIndex, int pageSize);
        ResponseResult<PaginatedList<TEntityDto>> GetAll(int pageIndex, int pageSize, Expression<Func<TEntityDto, int>> keySelector, OrderBy orderBy = OrderBy.Ascending);
        ResponseResult<PaginatedList<TEntityDto>> GetAll(int pageIndex, int pageSize, Expression<Func<TEntityDto, int>> keySelector, Expression<Func<TEntityDto, bool>> predicate, OrderBy orderBy);
        ResponseResult<PaginatedList<TEntityDto>> GetAll(int pageIndex, int pageSize, string keySelector, Expression<Func<TEntityDto, bool>> predicate, OrderBy orderBy);
        ResponseResult<TEntityDto> GetSingle(int id);
        ResponseResult<int> Count();
        ResponseResult<int> Count(Expression<Func<TEntityDto, bool>> predicate);
        ResponseResult<bool> Any();
        ResponseResult<bool> Any(Expression<Func<TEntityDto, bool>> predicate);
        ResponseResult<EntitySaveResult> Insert(TEntityDto entity);
        ResponseResult<EntitySaveResult> Update(TEntityDto entity);
        ResponseResult<EntitySaveResult> Delete(int id);

        Task<ResponseResult<TEntityDto>> FirstOrDefaultAsync();
        Task<ResponseResult<TEntityDto>> FirstOrDefaultAsync(Expression<Func<TEntityDto, bool>> predicate);
        Task<ResponseResult<TEntityDto>> LastOrDefaultAsync();
        Task<ResponseResult<TEntityDto>> LastOrDefaultAsync(Expression<Func<TEntityDto, bool>> predicate);
        Task<ResponseResult<TEntityDto>> GetSingleAsync(int id);
        Task<ResponseResult<int>> CountAsync();
        Task<ResponseResult<int>> CountAsync(Expression<Func<TEntityDto, bool>> predicate);
        Task<ResponseResult<bool>> AnyAsync();
        Task<ResponseResult<bool>> AnyAsync(Expression<Func<TEntityDto, bool>> predicate);
    }
}
