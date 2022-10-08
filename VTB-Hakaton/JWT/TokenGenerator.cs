using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApi.JWT
{
    public class TokenGenerator
    {
        public string GenerateToken(string secretKey, string issuer, string audience, double expirationInMinutes,
            IEnumerable<Claim> claims = null)
        {
            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256);

            var issueTime = DateTime.UtcNow;

            JwtSecurityToken token = new(
                issuer,
                audience,
                claims,
                issueTime,
                issueTime.AddMinutes(expirationInMinutes),
                credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
