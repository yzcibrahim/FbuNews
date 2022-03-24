using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace UserService
{
    public class Auth : IJwtAuth
    {
        private readonly string key;
        NewsDbContext _ctx;
        public Auth(IConfiguration configuration, NewsDbContext ctx)
        {
            key = configuration.GetValue<string>("appSettings:AuthKey");
            _ctx = ctx;
        }
        public string Authentication(string username, string password)
        {
            var usr = _ctx.UserAccounts.FirstOrDefault(c => c.UserName == username && c.Password == password);
            if (usr == null)
                return null;
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenKey = Encoding.ASCII.GetBytes(key);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, username),
                        new Claim("userId",usr.Id.ToString())
                    }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            // 5. Return Token from method
            return tokenHandler.WriteToken(token);
        }
    }
}
