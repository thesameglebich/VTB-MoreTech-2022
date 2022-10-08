using System;

namespace WebApi.DataAccessLayer.Models
{
    public class RefreshToken
    {
        public RefreshToken()
        {
            Id = new Guid();
        }

        public Guid Id { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        User User { get; set; }
    }
}
