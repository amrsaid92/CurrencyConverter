using CurrencyConverter.DomainEntities;
using Refit;

namespace CurrencyConverter.ServiceInterfaces
{
    public interface IFrankfurterRefitClient
    {
        [Get("/latest")]
        Task<CurrencyLatestBaseResponseDto> Latest([AliasAs("base")] string baseCurrency);

        [Get("/latest")]
        Task<CurrencyLatestBaseResponseDto> Convert(string from, string to, double amount);
        [Get("/{from}..{to}")]
        Task<CurrencyRangeBaseResponseDto> GetRange(string from, string to, [AliasAs("base")] string baseCurrency);
    }
}
