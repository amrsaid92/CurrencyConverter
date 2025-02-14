using CurrencyConverter.Core.BaseEntities;

namespace CurrencyConverter.DomainEntities.Account
{
    public class UserDto : BaseEntityDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<string> Roles { get; set; }
    }
}
