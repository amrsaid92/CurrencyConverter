using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrencyConverter.Core.Data;
using CurrencyConverter.Core.Logging;

namespace CurrencyConverter.Core.DomainEntities
{
    public class ResponseHandler<TResult>
    {
        public static ResponseResult<TResult> GetResult(ResultCodeStatus code, TResult data, string message = "")
        {
            return new ResponseResult<TResult>()
            {
                Result = new ResultCode(code, message),
                Data = data
            };
        }
        public static ResponseResult<TResult> GetResult(ResultCodeStatus code, string message = "")
        {
            return new ResponseResult<TResult>()
            {
                Result = new ResultCode(code, message)
            };
        }
        public static ResponseResult<TResult> GetResult(Exception ex, ILogger logger = null)
        {
            var errorId = logger?.Log(ex);
            return new ResponseResult<TResult>()
            {
                Result = new ResultCode(ResultCodeStatus.Error, ex.Message /*string.Format(Resources.ResultMessages.ErrorMessage, errorId)*/)
            };
        }
    }
}
