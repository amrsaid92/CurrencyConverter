using CurrencyConverter.Core.DomainEntities;
using CurrencyConverter.DomainEntities;
using CurrencyConverter.ServiceInterfaces;

namespace CurrencyConverter.Services
{
    public class CurrencyConvertService : ICurrencyConvertService
    {
        private readonly IFrankfurterRefitClient _frankfurterRefitClient;
        public CurrencyConvertService(IFrankfurterRefitClient frankfurterRefitClient)
        {
            _frankfurterRefitClient = frankfurterRefitClient;
        }

        public async Task<ResponseResult<CurrencyLatestBaseResponseDto>> Convert(string from, string to, double amount)
        {
            try
            {
                if (IsNotValidCall(from) || IsNotValidCall(to))
                    return ResponseHandler<CurrencyLatestBaseResponseDto>.GetResult(Core.Data.ResultCodeStatus.BadRequest);

                var result = await _frankfurterRefitClient.Convert(from, to, amount);
                if (result != null)
                    return ResponseHandler<CurrencyLatestBaseResponseDto>.GetResult(Core.Data.ResultCodeStatus.Success, result);
                else
                    return ResponseHandler<CurrencyLatestBaseResponseDto>.GetResult(Core.Data.ResultCodeStatus.Success, Resources.ResultMessages.ErrorMessage);
            }
            catch (Exception ex)
            {
                return ResponseHandler<CurrencyLatestBaseResponseDto>.GetResult(ex);
            }
        }

        public async Task<ResponseResult<CurrencyRangeBaseResponseDto>> GetRange(string fromDate, string toDate, string baseCurrency, int pageIndex = 0, int pageSize = 10)
        {
            try
            {
                if (IsNotValidCall(baseCurrency))
                    return ResponseHandler<CurrencyRangeBaseResponseDto>.GetResult(Core.Data.ResultCodeStatus.BadRequest);

                var result = await _frankfurterRefitClient.GetRange(fromDate, toDate, baseCurrency);
                if (result != null)
                {
                    result.Rates = result.Rates.Skip(pageIndex * pageSize).Take(pageSize).ToDictionary();
                    return ResponseHandler<CurrencyRangeBaseResponseDto>.GetResult(Core.Data.ResultCodeStatus.Success, result);
                }
                else
                    return ResponseHandler<CurrencyRangeBaseResponseDto>.GetResult(Core.Data.ResultCodeStatus.Success, Resources.ResultMessages.ErrorMessage);
            }
            catch (Exception ex)
            {
                return ResponseHandler<CurrencyRangeBaseResponseDto>.GetResult(ex);
            }
        }

        public async Task<ResponseResult<CurrencyLatestBaseResponseDto>> Latest(string baseCurrency)
        {
            try
            {
                if (IsNotValidCall(baseCurrency))
                    return ResponseHandler<CurrencyLatestBaseResponseDto>.GetResult(Core.Data.ResultCodeStatus.BadRequest);

                var result = await _frankfurterRefitClient.Latest(baseCurrency);
                if (result != null)
                    return ResponseHandler<CurrencyLatestBaseResponseDto>.GetResult(Core.Data.ResultCodeStatus.Success, result);
                else
                    return ResponseHandler<CurrencyLatestBaseResponseDto>.GetResult(Core.Data.ResultCodeStatus.Success, Resources.ResultMessages.ErrorMessage);
            }
            catch (Exception ex)
            {
                return ResponseHandler<CurrencyLatestBaseResponseDto>.GetResult(ex);
            }
        }

        private bool IsNotValidCall(string currency) => ConfigurationKeys.ExcludedCurrencies.Contains(currency);
    }
}
