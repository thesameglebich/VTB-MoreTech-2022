using System.Threading.Tasks;
using WebApi.DataAccessLayer.Models;
using WebApi.Models.Response;

namespace WebApi.JWT
{
    public class Authenticator
    {
        private readonly AccessTokenGenerator _accessTokenGenerator;
        private readonly RefreshTokenGenerator _refreshTokenGenerator;
        private readonly RefreshTokenRepository _refreshTokenRepository;

        public Authenticator(AccessTokenGenerator accessTokenGenerator, RefreshTokenGenerator refreshTokenGenerator, RefreshTokenRepository refreshTokenRepository)
        {
            _accessTokenGenerator = accessTokenGenerator;
            _refreshTokenGenerator = refreshTokenGenerator;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<AuthenticatedUserResponse> AuthenticateAsync(User user)
        {
            var accessToken = _accessTokenGenerator.GenerateToken(user);
            var refreshToken = _refreshTokenGenerator.GenerateToken();
            var refreshTokenDBO = new RefreshToken()
            {
                Token = refreshToken,
                UserId = user.Id,
            };
            await _refreshTokenRepository.CreateAsync(refreshTokenDBO);

            return new AuthenticatedUserResponse()
            {
                Id = user.Id.ToString(),
                GivenName = user.ShortName,
                RefreshToken = refreshToken,
                AccessToken = accessToken
            };
        }
    }
}
