
using JwtExampleIdentity.Abstract;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JwtExampleIdentity.Services
{
    public class TokenService : ITokenService
    {
        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var now = DateTime.UtcNow;
            var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secret-key-example"));
            var signinCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256);

            // Generate the jwt token

            var jwt = new JwtSecurityToken(
                signingCredentials: signinCredentials,
                claims: claims,
                notBefore: now,
                expires: now.AddDays(1)
                );
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
