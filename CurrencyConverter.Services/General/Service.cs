using CurrencyConverter.Core.BaseEntities;
using CurrencyConverter.Core.Data;
using CurrencyConverter.Core.DomainEntities;
using CurrencyConverter.Core.Logging;
using CurrencyConverter.RepositoryInterfaces.General;
using CurrencyConverter.ServiceInterfaces.General;
using System.Collections;
using System.Linq.Expressions;

namespace CurrencyConverter.Services.General
{
    public class Service<TEntity, TEntityDto> : IService<TEntityDto>
       where TEntity : BaseEntity
       where TEntityDto : BaseEntityDto
    {
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly IRepository<TEntity> _repository;
        protected readonly ILogger _logger;
        protected readonly IAutoMapper Mapper;
        private bool _disposed;

        public Service(IUnitOfWork unitOfWork, IAutoMapper mapper, ILogger logger)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
            _repository = UnitOfWork.Repository<TEntity>();
            _logger = logger;
        }

        public IService<TEntityDto> AsTracking()
        {
            _repository.AsTracking();
            return this;
        }

        public IService<TEntityDto> AsNoTracking()
        {
            _repository.AsNoTracking();
            return this;
        }

        public IService<TEntityDto> Include(params Expression<Func<TEntityDto, object>>[] includeProperties)
        {
            _repository.Include(includeProperties.Select(itm =>
            Mapper.MapExpressionAsInclude<TEntityDto, TEntity>(itm)).ToArray());
            return this;
        }

        public ResponseResult<TEntityDto> FirstOrDefault()
        {
            try
            {
                var result = Mapper.Map<TEntity, TEntityDto>(_repository.FirstOrDefault());
                return ResponseHandler<TEntityDto>.GetResult(ResultCodeStatus.Success, result);
            }
            catch (Exception ex)
            {
                return ResponseHandler<TEntityDto>.GetResult(ex, _logger);
            }
        }

        public ResponseResult<TEntityDto> FirstOrDefault(Expression<Func<TEntityDto, bool>> predicate)
        {
            try
            {
                var result = Mapper.Map<TEntity, TEntityDto>(_repository.FirstOrDefault(Mapper.MapPredictExpression<TEntityDto, TEntity>(predicate)));
                return ResponseHandler<TEntityDto>.GetResult(ResultCodeStatus.Success, result);
            }
            catch (Exception ex)
            {
                return ResponseHandler<TEntityDto>.GetResult(ex, _logger);
            }
        }

        public ResponseResult<TEntityDto> LastOrDefault()
        {
            try
            {
                var result = Mapper.Map<TEntity, TEntityDto>(_repository.LastOrDefault());
                return ResponseHandler<TEntityDto>.GetResult(ResultCodeStatus.Success, result);
            }
            catch (Exception ex)
            {
                return ResponseHandler<TEntityDto>.GetResult(ex, _logger);
            }
        }

        public ResponseResult<TEntityDto> LastOrDefault(Expression<Func<TEntityDto, bool>> predicate)
        {
            try
            {
                var result = Mapper.Map<TEntity, TEntityDto>(_repository.LastOrDefault(Mapper.MapPredictExpression<TEntityDto, TEntity>(predicate)));
                return ResponseHandler<TEntityDto>.GetResult(ResultCodeStatus.Success, result);
            }
            catch (Exception ex)
            {
                return ResponseHandler<TEntityDto>.GetResult(ex, _logger);
            }
        }

        public ResponseResult<List<TEntityDto>> GetAll()
        {
            try
            {
                var result = Mapper.Map<List<TEntity>, List<TEntityDto>>(_repository.GetAll().ToList());
                return ResponseHandler<List<TEntityDto>>.GetResult(ResultCodeStatus.Success, result);
            }
            catch (Exception ex)
            {
                return ResponseHandler<List<TEntityDto>>.GetResult(ex, _logger);
            }
        }

        public ResponseResult<List<TEntityDto>> GetAll(Expression<Func<TEntityDto, bool>> predicate)
        {
            try
            {
                var result = Mapper.Map<List<TEntity>, List<TEntityDto>>(_repository.GetAll(
                    Mapper.MapPredictExpression<TEntityDto, TEntity>(predicate)).ToList());
                return ResponseHandler<List<TEntityDto>>.GetResult(ResultCodeStatus.Success, result);
            }
            catch (Exception ex)
            {
                return ResponseHandler<List<TEntityDto>>.GetResult(ex, _logger);
            }
        }

        public ResponseResult<List<TEntityDto>> GetAll(Expression<Func<TEntityDto, bool>> predicate, OrderBy order)
        {
            try
            {
                var result = Mapper.Map<List<TEntity>, List<TEntityDto>>(_repository.GetAll(
                    Mapper.MapPredictExpression<TEntityDto, TEntity>(predicate), order).ToList());
                return ResponseHandler<List<TEntityDto>>.GetResult(ResultCodeStatus.Success, result);
            }
            catch (Exception ex)
            {
                return ResponseHandler<List<TEntityDto>>.GetResult(ex, _logger);
            }
        }
        public ResponseResult<List<TEntityDto>> GetAll(Expression<Func<TEntityDto, bool>> predicate, Expression<Func<TEntityDto, int>> keySelector, OrderBy order)
        {
            try
            {
                var result = Mapper.Map<List<TEntity>, List<TEntityDto>>(_repository.GetAll(
                    Mapper.MapPredictExpression<TEntityDto, TEntity>(predicate),
                    Mapper.MapExpression<TEntityDto, TEntity, int>(keySelector), order).ToList());
                return ResponseHandler<List<TEntityDto>>.GetResult(ResultCodeStatus.Success, result);
            }
            catch (Exception ex)
            {
                return ResponseHandler<List<TEntityDto>>.GetResult(ex, _logger);
            }
        }

        public ResponseResult<PaginatedList<TEntityDto>> GetAll(int pageIndex, int pageSize)
        {
            try
            {
                var result = Mapper.MapToDtoPaginatedList<TEntity, TEntityDto>(_repository.GetAll(pageIndex, pageSize));
                return ResponseHandler<PaginatedList<TEntityDto>>.GetResult(ResultCodeStatus.Success, result);
            }
            catch (Exception ex)
            {
                return ResponseHandler<PaginatedList<TEntityDto>>.GetResult(ex, _logger);
            }
        }

        public ResponseResult<PaginatedList<TEntityDto>> GetAll(int pageIndex, int pageSize,
            Expression<Func<TEntityDto, int>> keySelector, OrderBy orderBy = OrderBy.Ascending)
        {
            try
            {
                var result = Mapper.MapToDtoPaginatedList<TEntity, TEntityDto>(_repository.GetAll(pageIndex, pageSize,
                    Mapper.MapExpression<TEntityDto, TEntity, int>(keySelector), orderBy));
                return ResponseHandler<PaginatedList<TEntityDto>>.GetResult(ResultCodeStatus.Success, result);
            }
            catch (Exception ex)
            {
                return ResponseHandler<PaginatedList<TEntityDto>>.GetResult(ex, _logger);
            }
        }

        public ResponseResult<PaginatedList<TEntityDto>> GetAll(int pageIndex, int pageSize,
            Expression<Func<TEntityDto, int>> keySelector, Expression<Func<TEntityDto, bool>> predicate, OrderBy orderBy)
        {
            try
            {
                var result = Mapper.MapToDtoPaginatedList<TEntity, TEntityDto>(_repository.GetAll(pageIndex, pageSize,
                    Mapper.MapExpression<TEntityDto, TEntity, int>(keySelector),
                    Mapper.MapPredictExpression<TEntityDto, TEntity>(predicate),
                    orderBy));
                return ResponseHandler<PaginatedList<TEntityDto>>.GetResult(ResultCodeStatus.Success, result);
            }
            catch (Exception ex)
            {
                return ResponseHandler<PaginatedList<TEntityDto>>.GetResult(ex, _logger);
            }
        }

        public ResponseResult<PaginatedList<TEntityDto>> GetAll(int pageIndex, int pageSize, string keySelector,
            Expression<Func<TEntityDto, bool>> predicate, OrderBy orderBy)
        {
            try
            {
                var result = Mapper.MapToDtoPaginatedList<TEntity, TEntityDto>(_repository.GetAll(pageIndex, pageSize, keySelector,
                    Mapper.MapPredictExpression<TEntityDto, TEntity>(predicate),
                    orderBy));
                return ResponseHandler<PaginatedList<TEntityDto>>.GetResult(ResultCodeStatus.Success, result);
            }
            catch (Exception ex)
            {
                return ResponseHandler<PaginatedList<TEntityDto>>.GetResult(ex, _logger);
            }
        }

        public ResponseResult<TEntityDto> GetSingle(int id)
        {
            try
            {
                var result = Mapper.Map<TEntity, TEntityDto>(_repository.GetSingle(id));
                return ResponseHandler<TEntityDto>.GetResult(ResultCodeStatus.Success, result);
            }
            catch (Exception ex)
            {
                return ResponseHandler<TEntityDto>.GetResult(ex, _logger);
            }
        }

        public ResponseResult<int> Count()
        {
            try
            {
                return ResponseHandler<int>.GetResult(ResultCodeStatus.Success, _repository.Count());
            }
            catch (Exception ex)
            {
                return ResponseHandler<int>.GetResult(ex, _logger);
            }
        }

        public ResponseResult<int> Count(Expression<Func<TEntityDto, bool>> predicate)
        {
            try
            {
                return ResponseHandler<int>.GetResult(ResultCodeStatus.Success,
                    _repository.Count(Mapper.MapPredictExpression<TEntityDto, TEntity>(predicate)));
            }
            catch (Exception ex)
            {
                return ResponseHandler<int>.GetResult(ex, _logger);
            }
        }

        public ResponseResult<bool> Any()
        {
            try
            {
                return ResponseHandler<bool>.GetResult(ResultCodeStatus.Success, _repository.Any());
            }
            catch (Exception ex)
            {
                return ResponseHandler<bool>.GetResult(ex, _logger);
            }
        }

        public ResponseResult<bool> Any(Expression<Func<TEntityDto, bool>> predicate)
        {
            try
            {
                return ResponseHandler<bool>.GetResult(ResultCodeStatus.Success,
                    _repository.Any(Mapper.MapPredictExpression<TEntityDto, TEntity>(predicate)));
            }
            catch (Exception ex)
            {
                return ResponseHandler<bool>.GetResult(ex, _logger);
            }
        }

        public virtual ResponseResult<EntitySaveResult> Insert(TEntityDto entity)
        {
            try
            {
                SetCreation(entity);
                var newEntity = Mapper.Map<TEntityDto, TEntity>(entity);
                _repository.Insert(newEntity);
                int result = UnitOfWork.SaveChanges();
                return ResponseHandler<EntitySaveResult>.GetResult(ResultCodeStatus.Success,
                    new EntitySaveResult(result, newEntity.Id), Resources.ResultMessages.AddedSuccess);
            }
            catch (Exception ex)
            {
                return ResponseHandler<EntitySaveResult>.GetResult(ex, _logger);
            }
        }

        public virtual ResponseResult<EntitySaveResult> Update(TEntityDto entity)
        {
            try
            {
                SetModification(entity);
                var newEntity = Mapper.Map<TEntityDto, TEntity>(entity);
                _repository.Update(newEntity);
                int result = UnitOfWork.SaveChanges();
                return ResponseHandler<EntitySaveResult>.GetResult(ResultCodeStatus.Success,
                    new EntitySaveResult(result, newEntity.Id), Resources.ResultMessages.AddedSuccess);
            }
            catch (Exception ex)
            {
                return ResponseHandler<EntitySaveResult>.GetResult(ex, _logger);
            }
        }

        public virtual ResponseResult<EntitySaveResult> Delete(int id)
        {
            try
            {
                _repository.Delete(_repository.GetSingle(id));
                int result = UnitOfWork.SaveChanges();
                return ResponseHandler<EntitySaveResult>.GetResult(ResultCodeStatus.Success,
                    new EntitySaveResult(result, id), Resources.ResultMessages.DeleteSuccess);
            }
            catch (Exception ex)
            {
                return ResponseHandler<EntitySaveResult>.GetResult(ex, _logger);
            }
        }

        public async Task<ResponseResult<TEntityDto>> FirstOrDefaultAsync()
        {
            try
            {
                var result = await _repository.FirstOrDefaultAsync();
                var entity = Mapper.Map<TEntity, TEntityDto>(result);
                return ResponseHandler<TEntityDto>.GetResult(ResultCodeStatus.Success, entity);
            }
            catch (Exception ex)
            {
                return ResponseHandler<TEntityDto>.GetResult(ex, _logger);
            }
        }

        public async Task<ResponseResult<TEntityDto>> FirstOrDefaultAsync(Expression<Func<TEntityDto, bool>> predicate)
        {
            try
            {
                var result = await _repository.FirstOrDefaultAsync(Mapper.MapPredictExpression<TEntityDto, TEntity>(predicate));
                var entity = Mapper.Map<TEntity, TEntityDto>(result);
                return ResponseHandler<TEntityDto>.GetResult(ResultCodeStatus.Success, entity);
            }
            catch (Exception ex)
            {
                return ResponseHandler<TEntityDto>.GetResult(ex, _logger);
            }
        }

        public async Task<ResponseResult<TEntityDto>> LastOrDefaultAsync()
        {
            try
            {
                var result = await _repository.LastOrDefaultAsync();
                var entity = Mapper.Map<TEntity, TEntityDto>(result);
                return ResponseHandler<TEntityDto>.GetResult(ResultCodeStatus.Success, entity);
            }
            catch (Exception ex)
            {
                return ResponseHandler<TEntityDto>.GetResult(ex, _logger);
            }
        }

        public async Task<ResponseResult<TEntityDto>> LastOrDefaultAsync(Expression<Func<TEntityDto, bool>> predicate)
        {
            try
            {
                var result = await _repository.FirstOrDefaultAsync(Mapper.MapPredictExpression<TEntityDto, TEntity>(predicate));
                var entity = Mapper.Map<TEntity, TEntityDto>(result);
                return ResponseHandler<TEntityDto>.GetResult(ResultCodeStatus.Success, entity);
            }
            catch (Exception ex)
            {
                return ResponseHandler<TEntityDto>.GetResult(ex, _logger);
            }
        }

        public async Task<ResponseResult<TEntityDto>> GetSingleAsync(int id)
        {
            try
            {
                var result = await _repository.GetSingleAsync(id);
                var entity = Mapper.Map<TEntity, TEntityDto>(result);
                return ResponseHandler<TEntityDto>.GetResult(ResultCodeStatus.Success, entity);
            }
            catch (Exception ex)
            {
                return ResponseHandler<TEntityDto>.GetResult(ex, _logger);
            }
        }

        public async Task<ResponseResult<int>> CountAsync()
        {
            try
            {
                var result = await _repository.CountAsync();
                return ResponseHandler<int>.GetResult(ResultCodeStatus.Success, result);
            }
            catch (Exception ex)
            {
                return ResponseHandler<int>.GetResult(ex, _logger);
            }
        }

        public async Task<ResponseResult<int>> CountAsync(Expression<Func<TEntityDto, bool>> predicate)
        {
            try
            {
                var result = await _repository.CountAsync(Mapper.MapPredictExpression<TEntityDto, TEntity>(predicate));
                return ResponseHandler<int>.GetResult(ResultCodeStatus.Success, result);
            }
            catch (Exception ex)
            {
                return ResponseHandler<int>.GetResult(ex, _logger);
            }
        }

        public async Task<ResponseResult<bool>> AnyAsync()
        {
            try
            {
                var result = await _repository.AnyAsync();
                return ResponseHandler<bool>.GetResult(ResultCodeStatus.Success, result);
            }
            catch (Exception ex)
            {
                return ResponseHandler<bool>.GetResult(ex, _logger);
            }
        }

        public async Task<ResponseResult<bool>> AnyAsync(Expression<Func<TEntityDto, bool>> predicate)
        {
            try
            {
                var result = await _repository.AnyAsync(Mapper.MapPredictExpression<TEntityDto, TEntity>(predicate));
                return ResponseHandler<bool>.GetResult(ResultCodeStatus.Success, result);
            }
            catch (Exception ex)
            {
                return ResponseHandler<bool>.GetResult(ex, _logger);
            }
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                UnitOfWork.Dispose();
            }
            _disposed = true;
        }

        #region Help Functions
        protected void SetCreation(BaseEntityDto newEntity)
        {
            if (newEntity.GetType().GetProperty("CreatedAt") != null)
                newEntity.GetType().GetProperty("CreatedAt")?.SetValue(newEntity, DateTime.Now, null);
            var props = newEntity.GetType().GetProperties().Where(
            p =>
                (typeof(IEnumerable).IsAssignableFrom(p.PropertyType) || typeof(BaseEntityDto).IsAssignableFrom(p.PropertyType))
                && !typeof(string).IsAssignableFrom(p.PropertyType)).ToList();
            foreach (var prop in props)
            {
                var references = newEntity.GetType().GetProperty(prop.Name);
                if (references == null) continue;
                if (typeof(IEnumerable).IsAssignableFrom(prop.PropertyType))
                {
                    var enumerable = references?.GetValue(newEntity) as IEnumerable;
                    if (enumerable != null)
                        foreach (var reference in enumerable)
                        {
                            if (typeof(BaseEntityDto).IsAssignableFrom(reference.GetType()))
                                SetCreation((BaseEntityDto)reference);
                        }
                }
                else if (typeof(BaseEntityDto).IsAssignableFrom(prop.PropertyType))
                {
                    var entity = references?.GetValue(newEntity) as BaseEntityDto;
                    if (entity != null)
                        SetCreation(entity);
                }
            }
        }
        protected void SetModification(BaseEntityDto newEntity)
        {
            if (newEntity.GetType().GetProperty("ModifiedAt") != null)
                newEntity.GetType().GetProperty("ModifiedAt")?.SetValue(newEntity, DateTime.Now, null);
            var props = newEntity.GetType().GetProperties().Where(
            p =>
                (typeof(IEnumerable).IsAssignableFrom(p.PropertyType) || typeof(BaseEntityDto).IsAssignableFrom(p.PropertyType))
                && !typeof(string).IsAssignableFrom(p.PropertyType)).ToList();
            foreach (var prop in props)
            {
                var references = newEntity.GetType().GetProperty(prop.Name);
                if (references == null) continue;
                if (typeof(IEnumerable).IsAssignableFrom(prop.PropertyType))
                {
                    var enumerable = references?.GetValue(newEntity) as IEnumerable;
                    if (enumerable != null)
                        foreach (var reference in enumerable)
                        {
                            if (reference == null) continue;
                            if (typeof(BaseEntityDto).IsAssignableFrom(reference.GetType()))
                                if (reference.GetType().GetProperty("Id")?.GetValue(reference)?.ToString() != "0")
                                    SetCreation((BaseEntityDto)reference);
                                else
                                    SetModification((BaseEntityDto)reference);
                        }
                }
                else if (typeof(BaseEntityDto).IsAssignableFrom(prop.PropertyType))
                {
                    var entity = references?.GetValue(newEntity) as BaseEntityDto;
                    if (entity != null)
                        if (entity.Id == 0)
                            SetCreation(entity);
                        else
                            SetModification(entity);
                }
            }
        }

        public void InitializePreference(bool isArabic, int userId)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
