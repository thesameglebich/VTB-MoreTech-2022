using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebApi.DataAccessLayer;
using WebApi.DataAccessLayer.Models;
using WebApi.Models.CommonModels;

namespace WebApi.Models.Requests.User
{
    public class UserEditRequestModel
    {
        public int? UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? MiddleName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public Result<UserEditRequestModel> Init(int id, DB ctx, DataAccessLayer.Models.User current)
        {
            var user = ctx.Users.FirstOrDefault(x => x.Id == id);

            if (user is null)
            {
                return new Result<UserEditRequestModel>(HttpStatusCode.NotFound, new Error("Пользователь не найден"));
            }

            if (current.Id != id && current.Role != UserRole.Superadmin)
            {
                return new Result<UserEditRequestModel>(HttpStatusCode.Forbidden, new Error("Нет прав"));
            }

            var response = new UserEditRequestModel()
            {
                Name = user.Name,
                Surname = user.Surname,
                MiddleName = user.MiddleName,
                Phone = user.Phone,
                Email = user.Email
            };

            return new Result<UserEditRequestModel>(response);
        }
    }
}
