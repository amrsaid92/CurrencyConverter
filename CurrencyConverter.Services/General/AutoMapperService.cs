using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using CurrencyConverter.Core.BaseEntities;
using CurrencyConverter.Core.Data;
using CurrencyConverter.Core.DomainEntities;
using CurrencyConverter.DomainEntities;
using CurrencyConverter.DomainEntities.Account;
using CurrencyConverter.Entities;
using System.Linq.Expressions;

namespace CurrencyConverter.Services.General
{
    public class AutoMapperService : IAutoMapper
    {
        private MapperConfiguration _mapping = null;
        public AutoMapperService()
        {
            SetMappingConfiguration();
        }
        public AutoMapperService(MapperConfiguration mapping)
        {
            this._mapping = mapping;
        }
        public void SetMappingConfiguration(bool isArabic = true, int userId = 0)
        {
            this._mapping =
                new MapperConfiguration(
                    m =>
                    {
                        m.CreateMap<BaseEntity, BaseEntityDto>().ReverseMap();
                        m.CreateMap<User, UserDto>().ReverseMap();
                        m.CreateMap<CurrencyLog, CurrencyLogDto>().ReverseMap();
                    });
        }

        public PaginatedList<TResult> MapToDtoPaginatedList<TSource, TResult>(PaginatedList<TSource> paginatedList)
        {
            var dto = paginatedList.Data.Select(Map<TSource, TResult>).ToList();
            return new PaginatedList<TResult>(dto, paginatedList.PageIndex, paginatedList.PageSize,
                paginatedList.TotalCount);
        }

        public TResult Map<TSource, TResult>(TSource source)
        {
            var mapping = this._mapping.CreateMapper();
            var result = mapping.Map<TSource, TResult>(source);
            return result;
        }

        public Expression<Func<TResult, object>> MapExpressionAsInclude<TSource, TResult>(Expression<Func<TSource, object>> source)
        {
            var mapping = this._mapping.CreateMapper();
            var result = mapping.MapExpressionAsInclude<Expression<Func<TSource, object>>,
                Expression<Func<TResult, object>>>(source);
            return result;
        }

        public Expression<Func<TResult, bool>> MapPredictExpression<TSource, TResult>(Expression<Func<TSource, bool>> source)
        {
            var mapping = this._mapping.CreateMapper();
            var result = mapping.MapExpression<Expression<Func<TSource, bool>>, Expression<Func<TResult, bool>>>(source);
            return result;
        }

        public Expression<Func<TResult, TType>> MapExpression<TSource, TResult, TType>(Expression<Func<TSource, TType>> source)
        {
            var mapping = this._mapping.CreateMapper();
            var result = mapping.MapExpression<Expression<Func<TSource, TType>>, Expression<Func<TResult, TType>>>(source);
            return result;
        }
    }
}
