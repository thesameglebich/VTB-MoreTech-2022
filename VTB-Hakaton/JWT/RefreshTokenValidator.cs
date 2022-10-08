using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace WebApi.JWT
{
    public class RefreshTokenValidator
    {
        private readonly IConfiguration _config;
        private readonly JWTSettings _jwtSettings;
        public RefreshTokenValidator(IConfiguration config, JWTSettings jwtSettings)
        {
            _config = config;
            _jwtSettings = jwtSettings;
        }

        public bool Validate(string refreshToken)
        {
            TokenValidationParameters validationParams = new()
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:RefreshTokenSecret"])),
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ClockSkew = TimeSpan.Zero
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                tokenHandler.ValidateToken(refreshToken, validationParams, out SecurityToken validatedToken);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
