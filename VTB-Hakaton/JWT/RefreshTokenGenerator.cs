using Microsoft.Extensions.Configuration;

namespace WebApi.JWT
{
    public class RefreshTokenGenerator
    {
        private readonly IConfiguration _config;
        private readonly JWTSettings _jwtSettings;
        private readonly TokenGenerator _tokenGenerator;

        public RefreshTokenGenerator(IConfiguration config, JWTSettings jwtSettings, TokenGenerator tokenGenerator)
        {
            _config = config;
            _jwtSettings = jwtSettings;
            _tokenGenerator = tokenGenerator;
        }

        public string GenerateToken()
        {
            return _tokenGenerator.GenerateToken(
                    _config["JWT:RefreshTokenSecret"],
                    _jwtSettings.Issuer,
                    _jwtSettings.Audience,
                    _jwtSettings.RefreshTokenExpirationInMinutes);
        }
    }
}
