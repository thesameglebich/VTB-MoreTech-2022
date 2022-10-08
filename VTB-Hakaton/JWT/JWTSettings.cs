namespace WebApi.JWT
{
    public class JWTSettings
    {
        public double AccessTokenExpirationInMinutes { get; set; }
        public double RefreshTokenExpirationInMinutes { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
