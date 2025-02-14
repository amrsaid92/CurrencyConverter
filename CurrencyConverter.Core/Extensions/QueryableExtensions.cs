using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using CurrencyConverter.Core.DomainEntities;

namespace CurrencyConverter.Core.Extensions
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class QueryableExtensions
    {
        public static PaginatedList<T> ToPaginatedList<T>(this IQueryable<T> query, int pageIndex, int pageSize, int total)
        {
            var list = query.ToList();
            return new PaginatedList<T>(list, pageIndex, pageSize, total);
        }

        public static IQueryable<T> Paginate<T>(this IQueryable<T> query, int pageIndex, int pageSize)
        {
            var entities = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return entities;
        }
        public static IQueryable<TSource> OrderBy<TSource>(
            this IQueryable<TSource> query, string propertyName)
        {
            var propInfo = typeof(TSource).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (propInfo == null)
                propertyName = "Id";

            var queryElementTypeParam = Expression.Parameter(typeof(TSource));
            var memberAccess = Expression.PropertyOrField(queryElementTypeParam, propertyName);

            var keySelector = Expression.Lambda(memberAccess, queryElementTypeParam);

            var orderBy = Expression.Call(
                typeof(Queryable),
                "OrderBy",
                new Type[] { typeof(TSource), memberAccess.Type },
                query.Expression,
                Expression.Quote(keySelector));

            return query.Provider.CreateQuery<TSource>(orderBy);
        }
        public static IQueryable<TSource> OrderByDescending<TSource>(
            this IQueryable<TSource> query, string propertyName)
        {
            var propInfo = typeof(TSource).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (propInfo == null)
                propertyName = "Id";

            var queryElementTypeParam = Expression.Parameter(typeof(TSource));
            var memberAccess = Expression.PropertyOrField(queryElementTypeParam, propertyName);

            var keySelector = Expression.Lambda(memberAccess, queryElementTypeParam);

            var orderBy = Expression.Call(
                typeof(Queryable),
                "OrderByDescending",
                new Type[] { typeof(TSource), memberAccess.Type },
                query.Expression,
                Expression.Quote(keySelector));

            return query.Provider.CreateQuery<TSource>(orderBy);
        }
    }
}
