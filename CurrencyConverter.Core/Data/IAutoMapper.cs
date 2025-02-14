using System.Linq.Expressions;
using CurrencyConverter.Core.DomainEntities;

namespace CurrencyConverter.Core.Data
{
    public interface IAutoMapper
    {
        void SetMappingConfiguration(bool isArabic = true, int userId = 0);
        PaginatedList<TResult> MapToDtoPaginatedList<TSource, TResult>(PaginatedList<TSource> paginatedList);
        TResult Map<TSource, TResult>(TSource source);
        Expression<Func<TResult, object>> MapExpressionAsInclude<TSource, TResult>(Expression<Func<TSource, object>> source);
        Expression<Func<TResult, bool>> MapPredictExpression<TSource, TResult>(Expression<Func<TSource, bool>> source);
        Expression<Func<TResult, TType>> MapExpression<TSource, TResult, TType>(Expression<Func<TSource, TType>> source);
    }
}
