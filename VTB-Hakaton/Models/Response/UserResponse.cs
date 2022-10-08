using System;

namespace WebApi.Models.Response
{
    public class UserResponse
    {
        public UserResponse(DataAccessLayer.Models.User user)
        {
            Id = user.Id;
            Username = user.UserName;
            Fullname = user.FullName;
            Role = user.Role.ToString();
            Email = user.Email;
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
    }
}
