using CurrencyConverter.Core.Data;
using CurrencyConverter.Core.DomainEntities;
using CurrencyConverter.Core.Logging;
using CurrencyConverter.Core.Utilities;
using CurrencyConverter.DomainEntities;
using CurrencyConverter.DomainEntities.Account;
using CurrencyConverter.Entities;
using CurrencyConverter.RepositoryInterfaces.General;
using CurrencyConverter.ServiceInterfaces;
using CurrencyConverter.Services.General;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CurrencyConverter.Services
{
    public class UserService : Service<User, UserDto>, IUserService
    {
        public UserService(IUnitOfWork unitOfWork, IAutoMapper mapper, ILogger logger) : base(unitOfWork, mapper, logger)
        {
        }

        public ResponseResult<JwtTokenDto> GetToken(string email, string password)
        {
            try
            {
                var existUser = _repository.FirstOrDefault(itm => itm.Email == email);
                if (existUser == null)
                    return ResponseHandler<JwtTokenDto>.GetResult(ResultCodeStatus.Error, Resources.ResultMessages.ErrorMessage);
                else if (Hashing.VerifyHashString(password, existUser.Password))
                    return ResponseHandler<JwtTokenDto>.GetResult(ResultCodeStatus.Success, GenerateToken(existUser));
                else
                    return ResponseHandler<JwtTokenDto>.GetResult(ResultCodeStatus.Error, Resources.ResultMessages.ErrorMessage);

            }
            catch (Exception ex)
            {
                return ResponseHandler<JwtTokenDto>.GetResult(ex, _logger);
            }
        }

        public override ResponseResult<EntitySaveResult> Insert(UserDto entity)
        {
            try
            {

                var existUser = _repository.FirstOrDefault(itm => itm.Email == entity.Email);
                if (existUser == null)
                {
                    entity.Password = Hashing.HashString(entity.Password);
                    return base.Insert(entity);
                }
                else
                    return ResponseHandler<EntitySaveResult>.GetResult(ResultCodeStatus.Error, Resources.ResultMessages.ErrorMessage);
            }
            catch (Exception ex)
            {
                return ResponseHandler<EntitySaveResult>.GetResult(ex, _logger);
            }
        }

        private JwtTokenDto GenerateToken(User user)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationKeys.JWT.Secret));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>()
            {
                    new Claim(ClaimTypes.UserData, user.Name),
                    new Claim(ClaimTypes.NameIdentifier, user.Email ?? string.Empty),
                };
            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenOptions = new JwtSecurityToken(
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddDays(5),
                issuer: ConfigurationKeys.JWT.Issuer,
                audience: ConfigurationKeys.JWT.Audience,
                claims: claims,
                signingCredentials: signingCredentials
            );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return new JwtTokenDto
            {
                AccessToken = jwtToken,
                TokenType = "Bearer"
            };
        }
    }
}
