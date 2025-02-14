using CurrencyConverter.Core.Data;

namespace CurrencyConverter.Core.DomainEntities
{
    public class ResultCode
    {
        public ResultCode(ResultCodeStatus code, string message)
        {
            Code = code;
            Message = message;
        }
        public ResultCodeStatus Code { get; set; }
        public string Message { get; set; }
    }
}
