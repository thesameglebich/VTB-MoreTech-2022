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
using WebApi.Models.Requests.ActivitySolution;
using WebApi.Models.Response.ActivitySolution;

namespace WebApi.Controller
{
    [ApiController]
    [Authorize]
    [Route("/solution")]
    public class ActivitySolutionController : ControllerBase
    {
        private readonly DB _ctx;
        private readonly UserManager<User> _userManager;

        public ActivitySolutionController(DB ctx, UserManager<User> userManager)
        {
            _ctx = ctx;
            _userManager = userManager;
        }
        /*
        [HttpGet("getActivitySolutionInfo")]
        public Result<ActivitySolutionResponseModel> GetActivitySolutionInfo(int solutionId)
        {
            var solution = _ctx.ActivitySolutions
                .Include(x => x.Author)
                .FirstOrDefault(x => x.Id == solutionId);

            if (solution == null)
            {
                return new Result<ActivitySolutionResponseModel>(HttpStatusCode.NotFound,
                    new Error("Решения не существует"));
            }

            var response = new ActivitySolutionResponseModel
            {
                Id = solution.Id,
                SolutionText = solution.SolutionText,
                Author = new Models.DTO.UserDto {
                    Id = solution.AuthorId,
                    Email = solution.Author.Email,
                    FullName = solution.Author.FullName
                },
                SolutionType = solution.SolutionType,
                Verdict = solution.Verdict
            };

            return new Result<ActivitySolutionResponseModel>(response);
        }

        [HttpPost("create")]
        public Result<int> Create([FromBody] ActivitySolutionEditRequestModel model)
        {
            var author = _ctx.Users.FirstOrDefault(x => x.Id == model.AuthorId);

            if (author == null)
            {
                return new Result<int>(HttpStatusCode.NotFound,
                    new Error("автор не найден"));
            }
            
            var activity = _ctx.Groups.FirstOrDefault(x => x.Id == model.GroupId);

            if (group == null)
            {
                return new Result<int>(HttpStatusCode.NotFound,
                    new Error("группа не найдена"));
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

            return new Result<int>(0);
        }*/
    }
}
