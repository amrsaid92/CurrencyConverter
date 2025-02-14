using CurrencyConverter.Core.Data;

namespace CurrencyConverter.Core.DomainEntities
{
    public class PageListSearchDto
    {
        public PageListSearchDto()
        {
            this.Selector = "Id";
            this.Search = string.Empty;
        }
        public string Search { get; set; }
        public int PageIndex => Take != 0 ? (Skip / Take) + 1 : 0;
        public int Skip { get; set; }
        public int Take { get; set; }
        public string Selector { get; set; }
        public OrderBy Order { get; set; }
    }
}
