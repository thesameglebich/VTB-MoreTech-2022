using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebApi.DataAccessLayer;
using WebApi.DataAccessLayer.Models;
using WebApi.Models.CommonModels;
using WebApi.Models.Requests.User;
using WebApi.Models.Response.User;

namespace WebApi.Controller
{
    [ApiController]
    [Authorize]
    [Route("/user")]
    public class UserController : ControllerBase
    {
        private readonly DB _ctx;
        private readonly UserManager<User> _userManager;

        public UserController(DB ctx, UserManager<User> userManager)
        {
            _ctx = ctx;
            _userManager = userManager;
        }
        /*
        // Для dropdown листов
        [HttpGet("getAllUsersSelect")]
        public Result<List<UserBaseInfoResponseModel>> GetAllUsersSelect() =>
            new UserIndexResponseModel().InitStudents(_ctx);*/

        [HttpGet("getAllUsers")]
        public Result<List<UserBaseInfoResponseModel>> GetAllUsers(int page = 1) =>
            new UserIndexResponseModel().Init(_ctx, page);

        [HttpGet("edit")]
        public Result<UserEditRequestModel> Edit(int id) =>
            new UserEditRequestModel().Init(id, _ctx, _userManager.GetUserAsync(HttpContext.User).Result);

        [HttpPost("edit")]
        public async Task<Result> Edit([FromBody]UserEditRequestModel model)
        {
            if (model.UserId == null)
            {
                return new Result(HttpStatusCode.NotFound, new Error("id не передан"));
            }

            var user = _ctx.Users.FirstOrDefault(x => x.Id == model.UserId);

            if (user == null)
            {
                return new Result(HttpStatusCode.NotFound, new Error("Пользователь не найден"));
            }

            var _current = await _userManager.GetUserAsync(HttpContext.User);

            if (_current.Id != model.UserId && _current.Role != UserRole.Superadmin)
            {
                return new Result(HttpStatusCode.Forbidden, new Error("Нет прав"));
            }

            if (user.Email != model.Email)
            {
                var emailExist = await _userManager.FindByEmailAsync(model.Email) != null;

                return new Result(HttpStatusCode.Forbidden, new Error("Email уже занят"));
            }

            user.Name = model.Name;
            user.Surname = model.Surname;
            user.MiddleName = model.MiddleName;
            user.Phone = model.Phone;
            user.Email = model.Email;
            // ToDo: Пароль потом допишу
            _ctx.SaveChanges();
            return new Result(HttpStatusCode.OK);
        }

        [HttpGet("getUserBaseInfo")]
        public Result<UserBaseInfoResponseModel> GetUserBaseInfo(int id) =>
            new UserBaseInfoResponseModel().Init(id, _ctx, _userManager.GetUserAsync(HttpContext.User).Result);

    }

}
