using Microsoft.AspNetCore.Authorization;
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
using WebApi.Models.Requests.Activity;
using WebApi.Models.Response.Activity;

namespace WebApi.Controller
{
    [ApiController]
    [Authorize]
    [Route("/activity")]
    public class ActivityController : ControllerBase
    {
        private readonly DB _ctx;
        private readonly UserManager<User> _userManager;

        public ActivityController(DB ctx, UserManager<User> userManager)
        {
            _ctx = ctx;
            _userManager = userManager;
        }

        [HttpGet("getAllActivityInfos")]
        public Result<List<ActivityResponseModel>> GetAllActivityInfos(int groupId)
        {
            var activities = _ctx.Activities
                .Include(x => x.Author)
                .Where(x => x.GroupId == groupId)
                .Select(x => new ActivityResponseModel {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    GroupId = x.GroupId,
                    ActivityType = x.ActivityType,
                    RewardType = x.RewardType,
                    NftId = x.NftId,
                    RewardMoney = x.RewardMoney,
                    Author = x.Author != null ? new UserDto
                    {
                        Id = x.AuthorId.Value,
                        Email = x.Author.Email,
                        FullName = x.Author.FullName
                    } : null
                })
                .ToList();

            return new Result<List<ActivityResponseModel>>(activities);
        }

        [HttpGet("getActivityInfo")]
        public Result<ActivityResponseModel> GetActivityInfo(int activityId)
        {
            var activity = _ctx.Activities
                .Include(x => x.Author)
                .FirstOrDefault(x => x.Id == activityId);

            if (activity == null)
            {
                return new Result<ActivityResponseModel>(HttpStatusCode.NotFound,
                    new Error("Активности не существует"));
            }

            var response = new ActivityResponseModel
            {
                Id = activity.Id,
                Name = activity.Name,
                Description = activity.Description,
                GroupId = activity.GroupId,
                ActivityType = activity.ActivityType,
                RewardType = activity.RewardType,
                NftId = activity.NftId,
                RewardMoney = activity.RewardMoney,
                Author = activity.Author != null ? new UserDto
                {
                    Id = activity.AuthorId.Value,
                    Email = activity.Author.Email,
                    FullName = activity.Author.FullName
                } : null
            };

            return new Result<ActivityResponseModel>(response);
        }

        [HttpPost("create")]
        public Result<int> Create([FromBody] ActivityEditRequestModel model)
        {
            var author = _ctx.Users.FirstOrDefault(x => x.Id == model.AuthorId);

            if (author == null)
            {
                return new Result<int>(HttpStatusCode.NotFound,
                    new Error("автор не найден"));
            }

            Group group = null;

            if (model.ActivityType == ActivityType.Group)
            {
                group = _ctx.Groups.FirstOrDefault(x => x.Id == model.GroupId);

                if (group == null)
                {
                    return new Result<int>(HttpStatusCode.NotFound,
                        new Error("группа не найдена"));
                }
            }

            var activity = new Activity
            {
                Name = model.Name,
                Description = model.Description,
                Group = group,
                Author = author,
                ActivityType = model.ActivityType,
                RewardType = model.RewardType,
                RewardMoney = model.RewardMoney,
                NftId = model.NftId
            };

            _ctx.Activities.Add(activity);
            _ctx.SaveChanges();

            return new Result<int>(activity.Id);
        }

        [HttpGet("edit")]
        public Result<ActivityEditRequestModel> Edit(int groupId)
        {
            var activity = _ctx.Activities.FirstOrDefault(x => x.Id == groupId);

            if (activity == null)
            {
                return new Result<ActivityEditRequestModel>(HttpStatusCode.NotFound,
                    new Error("Активность не найден"));
            }

            var response = new ActivityEditRequestModel
            {
                Id = activity.Id,
                Name = activity.Name,
                Description = activity.Description,
                RewardType = activity.RewardType,
                NftId = activity.NftId,
                RewardMoney = activity.RewardMoney,
            };

            return new Result<ActivityEditRequestModel>(response);
        }

        [HttpPost("edit")]
        public Result Edit([FromBody] ActivityEditRequestModel model)
        {
            if (model.Id == null || model.Id < 0)
            {
                return new Result(HttpStatusCode.NotFound,
                    new Error("Id не передан"));
            }

            var activity = _ctx.Activities.FirstOrDefault(x => x.Id == model.Id);

            if (activity == null)
            {
                return new Result(HttpStatusCode.NotFound,
                    new Error("Активность не найдена"));
            }

            activity.Name = model.Name;
            activity.Description = model.Description;
            activity.RewardMoney = model.RewardMoney;
            activity.NftId = model.NftId;

            _ctx.SaveChanges();

            return new Result(HttpStatusCode.OK);
        }

        [HttpPost("delete")]
        public Result Delete(int activityId)
        {
            var activity = _ctx.Activities.FirstOrDefault(x => x.Id == activityId);

            if (activity == null)
            {
                return new Result(HttpStatusCode.NotFound,
                    new Error("Активность не существует"));
            }

            _ctx.Remove(activity);
            _ctx.SaveChanges();

            return new Result(HttpStatusCode.OK);
        }
    }
}
