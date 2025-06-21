using Alatha_API.Models;
using Alatha_Classes.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Alatha_API.Services
{
    public class JwtService
    {
        private readonly AlathaTrasportiContext _dbContext;
        private readonly IConfiguration _config;

        public JwtService(AlathaTrasportiContext context, IConfiguration config)
        {
            _dbContext = context;
            _config = config;
        }

        public async Task<LoginResponse?> Authenticate(LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password)) return null;

            var userAccount = await _dbContext.UserAccounts.FirstOrDefaultAsync(x => x.UserName == request.Username);
            if (userAccount is null) return null;
            bool isValidPassword = PasswordHasher.VerifyPassword(request.Password, userAccount.Password);
            if (!isValidPassword) return null;

            var issuer = _config["JwtConfig:Issuer"];
            var audience = _config["JwtConfig:Audience"];
            var tokenValidityMins = _config.GetValue<int>("JwtConfig:TokenValidityMins");
            var tokenExpiryTimeStamp = DateTime.UtcNow.AddMinutes(tokenValidityMins);
            var key = _config["JwtConfig:Key"];

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, userAccount.UserName),
                    new Claim(ClaimTypes.Role, userAccount.Role)
                }),
                Expires = tokenExpiryTimeStamp,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                                                        SecurityAlgorithms.HmacSha512Signature),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(securityToken);

            return new LoginResponse()
            {
                AccessToken = accessToken,
                Username = request.Username,
                ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.UtcNow).TotalSeconds
            };
        }
    }
}
