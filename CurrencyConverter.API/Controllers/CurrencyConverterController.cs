using Asp.Versioning;
using CurrencyConverter.Core.DomainEntities;
using CurrencyConverter.DomainEntities;
using CurrencyConverter.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConverter.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    //[EnableRateLimiting("RateLimitPolicy")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [ApiController]
    [Authorize]
    public class CurrencyConverterController : ControllerBase
    {
        private readonly ICurrencyConvertService _currencyConvertService;

        public CurrencyConverterController(ICurrencyConvertService currencyConvertService)
        {
            _currencyConvertService = currencyConvertService;
        }
        [HttpGet("Latest")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(ResponseResult<CurrencyLatestBaseResponseDto>), 200)]
        [Authorize(Roles = "User,Admin")]
        public async Task<ResponseResult<CurrencyLatestBaseResponseDto>> LatestV1(string baseCurrency)
        {
            return await _currencyConvertService.Latest(baseCurrency);
        }

        [HttpGet("Latest")]
        [MapToApiVersion("2.0")]
        [ProducesResponseType(typeof(ResponseResult<CurrencyLatestBaseResponseDto>), 200)]
        [ProducesResponseType(typeof(BadRequest), 400)]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> LatestV2(string baseCurrency)
        {
            var result = await _currencyConvertService.Latest(baseCurrency);
            if (result.Result.Code == Core.Data.ResultCodeStatus.BadRequest)
                return BadRequest();
            return Ok(result);
        }

        [HttpGet("Convert")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(ResponseResult<CurrencyLatestBaseResponseDto>), 200)]
        [ProducesResponseType(typeof(BadRequest), 400)]
        [AllowAnonymous]
        public async Task<IActionResult> Convert(string from, string to, double amount)
        {
            var result = await _currencyConvertService.Convert(from, to, amount);
            if (result.Result.Code == Core.Data.ResultCodeStatus.BadRequest)
                return BadRequest();
            return Ok(result);
        }

        [HttpGet("GetRange")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(ResponseResult<CurrencyRangeBaseResponseDto>), 200)]
        [ProducesResponseType(typeof(BadRequest), 400)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetRange(string fromDate, string toDate, string baseCurrency, int pageIndex = 0, int pageSize = 10)
        {
            var result = await _currencyConvertService.GetRange(fromDate, toDate, baseCurrency, pageIndex, pageSize);
            if (result.Result.Code == Core.Data.ResultCodeStatus.BadRequest)
                return BadRequest();
            return Ok(result);
        }
    }
}
