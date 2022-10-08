using System;
using System.Linq.Expressions;

namespace WebApi.Models.DTO
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }

        public static Expression<Func<DataAccessLayer.Models.User, UserDto>> Projection
        {
            get
            {
                return u => new UserDto()
                {
                    Email = u.Email,
                    Id = u.Id,
                    FullName = u.FullName
                };
            }
        }

        public static Func<DataAccessLayer.Models.User, UserDto> Func()
        {
            return u => new UserDto()
                   {
                       Email = u.Email,
                       Id = u.Id,
                       FullName = u.FullName
                   };
        }
    }
}
