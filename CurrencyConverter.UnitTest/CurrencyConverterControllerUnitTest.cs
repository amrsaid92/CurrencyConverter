using CurrencyConverter.API.Controllers;
using CurrencyConverter.Core.Data;
using CurrencyConverter.Core.DomainEntities;
using CurrencyConverter.DomainEntities;
using CurrencyConverter.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CurrencyConverter.UnitTest
{
    public class CurrencyConverterControllerUnitTest
    {
        private readonly Mock<ICurrencyConvertService> _currencyConvertService;
        private readonly CurrencyConverterController _currencyConverterController;

        public CurrencyConverterControllerUnitTest()
        {
            _currencyConvertService = new Mock<ICurrencyConvertService>();
            _currencyConverterController = new CurrencyConverterController(_currencyConvertService.Object);
        }

        [Fact]
        public async Task LatestV1_ReturnOkResult()
        {
            _currencyConvertService.Setup(x => x.Latest("USD")).ReturnsAsync(new ResponseResult<CurrencyLatestBaseResponseDto>
            {
                Result = new ResultCode(ResultCodeStatus.Success, string.Empty),
                Data = new CurrencyLatestBaseResponseDto()
            });
            var result = await _currencyConverterController.LatestV1("USD");

            Assert.Equal(ResultCodeStatus.Success, result.Result.Code);
        }
        [Fact]
        public async Task LatestV1_ReturnBadResult()
        {
            _currencyConvertService.Setup(x => x.Latest("TRY")).ReturnsAsync(new ResponseResult<CurrencyLatestBaseResponseDto>
            {
                Result = new ResultCode(ResultCodeStatus.BadRequest, string.Empty),
                Data = new CurrencyLatestBaseResponseDto()
            });
            var result = await _currencyConverterController.LatestV1("TRY");

            Assert.Equal(ResultCodeStatus.BadRequest, result.Result.Code);
        }

        [Fact]
        public async Task LatestV2_ReturnBadResult()
        {
            _currencyConvertService.Setup(x => x.Latest("TRY")).ReturnsAsync(new ResponseResult<CurrencyLatestBaseResponseDto>
            {
                Result = new ResultCode(ResultCodeStatus.BadRequest, string.Empty),
                Data = new CurrencyLatestBaseResponseDto()
            });
            var result = await _currencyConverterController.LatestV2("TRY");

            var okResult = Assert.IsType<BadRequestResult>(result);
            var statusCode = okResult.StatusCode;
            Assert.Equal(400, statusCode);

        }
        [Fact]
        public async Task LatestV2_ReturnOkResult()
        {
            _currencyConvertService.Setup(x => x.Latest("USD")).ReturnsAsync(new ResponseResult<CurrencyLatestBaseResponseDto>
            {
                Result = new ResultCode(ResultCodeStatus.Success, string.Empty),
                Data = new CurrencyLatestBaseResponseDto()
            });
            var result = await _currencyConverterController.LatestV2("USD");

            var okResult = Assert.IsType<OkObjectResult>(result);
            var statusCode = okResult.StatusCode;
            Assert.Equal(200, statusCode);

            var actualData = Assert.IsType<ResponseResult<CurrencyLatestBaseResponseDto>>(okResult.Value);
            Assert.Equal(ResultCodeStatus.Success, actualData.Result.Code);
        }
    }
}