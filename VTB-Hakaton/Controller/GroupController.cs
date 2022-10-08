using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebApi.DataAccessLayer;
using WebApi.DataAccessLayer.Models;
using WebApi.Models.CommonModels;
using WebApi.Models.DTO;
using WebApi.Models.Requests.Group;
using WebApi.Models.Response.Group;

namespace WebApi.Controller
{
    [ApiController]
    [Authorize]
    [Route("/group")]
    public class GroupController : ControllerBase
    {
        private readonly DB _ctx;
        private readonly UserManager<User> _userManager;

        public GroupController(DB ctx, UserManager<User> userManager)
        {
            _ctx = ctx;
            _userManager = userManager;
        }

        [HttpGet("getGroupInfo")]
        public Result<GroupResponseModel> GetGroupInfo(int groupId)
        {
            var group = _ctx.Groups
                .Include(x => x.Users)
                .Include(x => x.Lead)
                .FirstOrDefault(x => x.Id == groupId);

            if (group == null)
            {
                return new Result<GroupResponseModel>(HttpStatusCode.NotFound,
                    new Error("Группа не существует"));
            }

            var response = new GroupResponseModel
            {
                Id = group.Id,
                Name = group.Name,
                Description = group.Description,
                Leader = new UserDto
                {
                    Id = group.Lead.Id,
                    Email = group.Lead.Email,
                    FullName = group.Lead.FullName
                },
                Users = group.Users
                    .Select(x => new UserDto
                    {
                        Id = x.Id,
                        Email = x.Email,
                        FullName = x.FullName
                    })
                    .ToList()
            };
            return new Result<GroupResponseModel>(response);
        }

        [HttpPost("create")]
        public Result<int> Create([FromBody]GroupEditRequestModel model)
        {
            var groupMembers = _ctx.Users
                .Where(x => model.WorkerIds.Contains(x.Id))
                .ToList();

            var leader = _ctx.Users.FirstOrDefault(x => x.Id == model.Id);

            if (leader == null)
            {
                return new Result<int>(HttpStatusCode.NotFound,
                    new Error("Руководителя не существует"));
            }

            var group = new Group
            {
                Name = model.Name,
                Description = model.Description,
                Users = groupMembers,
                Lead = leader
            };

            _ctx.Groups.Add(group);
            _ctx.SaveChanges();

            return new Result<int>(group.Id);
        }

        [HttpGet("edit")]
        public Result<GroupEditRequestModel> Edit(int groupId)
        {
            var group = _ctx.Groups
                .Include(x => x.Users)
                .Include(x => x.Lead)
                .FirstOrDefault(x => x.Id == groupId);

            if (group == null)
            {
                return new Result<GroupEditRequestModel>(HttpStatusCode.NotFound,
                    new Error("Группа не найден"));
            }

            var response = new GroupEditRequestModel
            {
                Id = group.Id,
                Name = group.Name,
                Description = group.Description,
                LeaderId = group.LeadId ?? 0,
                WorkerIds = group.Users
                    .Select(x => x.Id)
                    .ToList()
            };

            return new Result<GroupEditRequestModel>(response);
        }

        [HttpPost("edit")]
        public Result Edit([FromBody]GroupEditRequestModel model)
        {
            if (model.Id == null || model.Id < 0)
            {
                return new Result(HttpStatusCode.NotFound,
                    new Error("Id не передан"));
            }

            var group = _ctx.Groups.FirstOrDefault(x => x.Id == model.Id);

            if (group == null)
            {
                return new Result(HttpStatusCode.NotFound,
                    new Error("Группа не найден"));
            }

            var groupMembers = _ctx.Users
                .Where(x => model.WorkerIds.Contains(x.Id))
                .ToList();

            var leader = _ctx.Users.FirstOrDefault(x => x.Id == model.Id);

            if (leader == null)
            {
                return new Result(HttpStatusCode.NotFound,
                    new Error("Руководителя не существует"));
            }

            group.Name = model.Name;
            group.Description = model.Description;
            group.Lead = leader;
            //group.Users = groupMembers;

            _ctx.SaveChanges();

            return new Result(HttpStatusCode.OK);
        }

        [HttpPost("delete")]
        public Result Delete(int groupId)
        {
            var group = _ctx.Groups.FirstOrDefault(x => x.Id == groupId);

            if (group == null)
            {
                return new Result(HttpStatusCode.NotFound,
                    new Error("Группа не существует"));
            }

            _ctx.Remove(group);
            _ctx.SaveChanges();

            return new Result(HttpStatusCode.OK);
        }
    }
}
