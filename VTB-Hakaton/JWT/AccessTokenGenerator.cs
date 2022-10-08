using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Security.Claims;
using WebApi.DataAccessLayer.Models;

namespace WebApi.JWT
{
    public class AccessTokenGenerator
    {
        private readonly IConfiguration _config;
        private readonly JWTSettings _jwtSettings;
        private readonly TokenGenerator _tokenGenerator;

        public AccessTokenGenerator(IConfiguration config, JWTSettings jwtSettings, TokenGenerator tokenGenerator)
        {
            _config = config;
            _jwtSettings = jwtSettings;
            _tokenGenerator = tokenGenerator;
        }

        public string GenerateToken(User user)
        {

            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.GivenName, user.ShortName)
            };

            return _tokenGenerator.GenerateToken(
                _config["JWT:AccessTokenSecret"],
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                _jwtSettings.AccessTokenExpirationInMinutes,
                claims);
        }
    }
}
