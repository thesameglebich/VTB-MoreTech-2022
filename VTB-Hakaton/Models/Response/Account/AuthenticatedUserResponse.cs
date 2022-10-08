namespace WebApi.Models.Response
{
    public class AuthenticatedUserResponse
    {
        public string GivenName { get; set; }
        public string Id { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
