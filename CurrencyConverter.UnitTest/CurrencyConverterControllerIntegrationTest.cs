using CurrencyConverter.Core.Data;
using CurrencyConverter.Core.DomainEntities;
using CurrencyConverter.DomainEntities;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace CurrencyConverter.UnitTest
{
    public class CurrencyConverterControllerIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private const string AdminToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3VzZXJkYXRhIjoiQW1yIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiJhbXIuc2FpZDkyQGdtYWlsLmNvbSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwibmJmIjoxNzM5NjA1ODEyLCJleHAiOjE3NDAwMzc4MTIsImlzcyI6IlRlc3RJc3N1ZXIiLCJhdWQiOiJDb252ZXJ0ZXJBcHBsaWNhdGlvbiJ9.ihrIGl6tNveqPaDhSQkoeUVPbmwfE83C4LER8IQ2VT0";

        public CurrencyConverterControllerIntegrationTest(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task LatestV1_ReturnUnAuthorizedResult()
        {
            var requestUri = "/api/v1/CurrencyConverter/Latest?baseCurrency=USD";

            var response = await _client.GetAsync(requestUri);

            Assert.True(response.StatusCode == System.Net.HttpStatusCode.Unauthorized);
        }
        [Fact]
        public async Task LatestV1_ReturnOkResult()
        {
            var requestUri = "/api/v1/CurrencyConverter/Latest?baseCurrency=USD";
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {AdminToken}");

            var response = await _client.GetAsync(requestUri);

            response.EnsureSuccessStatusCode();
            var responseObject = await response.Content.ReadFromJsonAsync<ResponseResult<CurrencyLatestBaseResponseDto>>();
            Assert.NotNull(responseObject?.Data);
        }

        [Fact]
        public async Task LatestV2_ReturnOkResult()
        {
            var requestUri = "/api/v2/CurrencyConverter/Latest?baseCurrency=USD";
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {AdminToken}");

            var response = await _client.GetAsync(requestUri);

            response.EnsureSuccessStatusCode();
            var responseObject = await response.Content.ReadFromJsonAsync<ResponseResult<CurrencyLatestBaseResponseDto>>();
            Assert.NotNull(responseObject?.Data);
        }

        [Fact]
        public async Task LatestV2_ReturnBadResult()
        {
            var requestUri = "/api/v2/CurrencyConverter/Latest?baseCurrency=TYR";
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {AdminToken}");

            var response = await _client.GetAsync(requestUri);

            Assert.True(response.StatusCode == System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Convert_ReturnsOkResultWithConvertedAmount()
        {
            var requestData = new
            {
                amount = 100,
                from = "USD",
                to = "EUR"
            };
            var requestUri = $"/api/v1/CurrencyConverter/convert?amount={requestData.amount}&from={requestData.from}&to={requestData.to}";

            var response = await _client.GetAsync(requestUri);

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseResult<CurrencyLatestBaseResponseDto>>(responseString);
            Assert.NotNull(result);
            Assert.Equal(requestData.from, result.Data.Base);
            Assert.True(result.Data.Rates.EUR > 0);
        }

        [Fact]
        public async Task ConvertCurrency_MissingAmount_ReturnsErrorCode()
        {
            var requestData = new
            {
                from = "USD",
                to = "EUR"
            };
            var requestUri = $"/api/v1/CurrencyConverter/convert?to={requestData.to}";

            var response = await _client.GetAsync(requestUri);

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseResult<CurrencyLatestBaseResponseDto>>(responseString);

            Assert.NotNull(result);
            Assert.Equal(result?.Result.Code, ResultCodeStatus.Error);
        }

        [Fact]
        public async Task ConvertCurrency_InvalidCurrencyCodes_ReturnsErrorCode()
        {
            var requestData = new
            {
                amount = 100,
                from = "XXX",
                to = "XXX"
            };
            var requestUri = $"/api/v1/CurrencyConverter/convert?amount={requestData.amount}&from={requestData.from}&to={requestData.to}";

            var response = await _client.GetAsync(requestUri);

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseResult<CurrencyLatestBaseResponseDto>>(responseString);

            Assert.NotNull(result);
            Assert.Equal(result?.Result.Code, ResultCodeStatus.Error);
        }
    }
}