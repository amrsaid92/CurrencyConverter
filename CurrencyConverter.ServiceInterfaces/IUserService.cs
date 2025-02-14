using CurrencyConverter.Core.DomainEntities;
using CurrencyConverter.DomainEntities.Account;
using CurrencyConverter.ServiceInterfaces.General;

namespace CurrencyConverter.ServiceInterfaces
{
    public interface IUserService : IService<UserDto>
    {
        ResponseResult<JwtTokenDto> GetToken(string emai, string password);
    }
}
