using Microsoft.Extensions.DependencyInjection;
using WebApi.JWT;

namespace WebApi.Extensions.IServiceCollectionExtensions
{
    public static class JWTExtensions
    {
        public static void AddJwt(this IServiceCollection services, JWTSettings jwtSettings)
        {
            services.AddSingleton(jwtSettings);
            services.AddSingleton<TokenGenerator>();
            services.AddSingleton<AccessTokenGenerator>();
            services.AddSingleton<RefreshTokenGenerator>();
            services.AddScoped<RefreshTokenRepository>();
            services.AddScoped<RefreshTokenValidator>();
            services.AddScoped<Authenticator>();
        }
    }
}
