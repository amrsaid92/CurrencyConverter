using CurrencyConverter.Core.Data;
using CurrencyConverter.Core.Logging;

namespace CurrencyConverter.Core.DomainEntities
{
    public class ResponseResult<TResult>
    {
        public ResultCode Result { get; set; }
        public TResult Data { get; set; }
    }
}
