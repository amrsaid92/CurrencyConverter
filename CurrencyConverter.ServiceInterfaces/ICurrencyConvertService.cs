using CurrencyConverter.Core.DomainEntities;
using CurrencyConverter.DomainEntities;

namespace CurrencyConverter.ServiceInterfaces
{
    public interface ICurrencyConvertService
    {
        Task<ResponseResult<CurrencyLatestBaseResponseDto>> Latest(string baseCurrency);
        Task<ResponseResult<CurrencyLatestBaseResponseDto>> Convert(string from, string to, double amount);
        Task<ResponseResult<CurrencyRangeBaseResponseDto>> GetRange(string fromDate, string toDate, string baseCurrency, int pageIndex = 0, int pageSize = 10);
    }
}
