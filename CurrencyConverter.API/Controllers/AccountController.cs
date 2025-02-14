using CurrencyConverter.Core.Data;
using CurrencyConverter.DomainEntities.Account;
using CurrencyConverter.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CurrencyConverter.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult AddUser(UserDto user)
        {
            return Ok(_userService.Insert(user));
        }

        [HttpPost]
        public IActionResult Token([Required] string email, [Required] string password)
        {
            var httpContext = HttpContext.Request;
            var result = _userService.GetToken(email, password);
            if (result.Result.Code == ResultCodeStatus.Success)
                return Ok(result.Data);
            else
            {
                var errorDetails = new
                {
                    Error = "invalid_profile",
                    ErrorDescription = result.Result.Message
                };

                return BadRequest(errorDetails);
            }

        }
    }
}
