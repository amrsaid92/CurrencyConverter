using CurrencyConverter.Core.Data;

namespace CurrencyConverter.Core.DomainEntities
{
    public class PaginatedList<T>
    {
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public int TotalPageCount { get; private set; }
        public IEnumerable<T> Data { get; private set; }
        public bool HasPreviousPage => (PageIndex > 1);

        public bool HasNextPage => (PageIndex < TotalPageCount);

        public void OrderBy(Func<T, int> keySelector, OrderBy orderBy)
        {
            Data = orderBy == Core.Data.OrderBy.Ascending
                ? Data.OrderBy(keySelector)
                : Data.OrderByDescending(keySelector);
        }
        public PaginatedList(IEnumerable<T> source, int pageIndex, int pageSize, int totalCount)
        {
            Data = source ?? throw new ArgumentNullException(nameof(source));
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPageCount = (int)Math.Ceiling(totalCount / (double)pageSize);
        }
    }
}
