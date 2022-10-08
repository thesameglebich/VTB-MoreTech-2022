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
                    Id = solution.AuthorId.Value,
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
            
            var activity = _ctx.Activities.FirstOrDefault(x => x.Id == model.ActivityId);

            if (activity == null)
            {
                return new Result<int>(HttpStatusCode.NotFound,
                    new Error("активность не найдена"));
            }

            var solution = new ActivitySolution
            {
                SolutionText = model.SolutionText,
                Author = author,
                SolutionType = SolutionType.WaitingApprove,
                Activity = activity
            };

            _ctx.ActivitySolutions.Add(solution);
            _ctx.SaveChanges();

            return new Result<int>(solution.Id);
        }

        [HttpGet("edit")]
        public Result<ActivitySolutionEditRequestModel> Edit(int id)
        {
            var solution = _ctx.ActivitySolutions.FirstOrDefault(x => x.Id == id);

            if (solution == null)
            {
                return new Result<ActivitySolutionEditRequestModel>(HttpStatusCode.NotFound,
                    new Error("решение не найдено"));
            }

            return new Result<ActivitySolutionEditRequestModel>(new ActivitySolutionEditRequestModel
            {
                Id = id,
                SolutionText = solution.SolutionText,
            });
        }

        [HttpPost("edit")]
        public Result Edit([FromBody] ActivitySolutionEditRequestModel model)
        {
            var solution = _ctx.ActivitySolutions.FirstOrDefault(x => x.Id == model.Id);

            if (solution == null)
            {
                return new Result(HttpStatusCode.NotFound,
                    new Error("решение не найдено"));
            }

            solution.SolutionText = model.SolutionText;

            _ctx.SaveChanges();

            return new Result(HttpStatusCode.OK);
        }

        [HttpPost("setSolutionType")]
        public Result SetSolutionType([FromBody] SetSolutionRequestModel model)
        {
            var solution = _ctx.ActivitySolutions.FirstOrDefault(x => x.Id == model.ActivityId);

            if (solution == null)
            {
                return new Result(HttpStatusCode.NotFound,
                    new Error("Решение не найдено"));
            }

            if (solution.SolutionType == SolutionType.Approved)
            {
                return new Result(HttpStatusCode.NotFound,
                    new Error("Изменить оценку нельзя"));
            }

            solution.SolutionType = model.SolutionType;
            solution.Verdict = model.Verdict;

            _ctx.SaveChanges();

            return new Result(HttpStatusCode.OK);
        }

        public class SetSolutionRequestModel
        {
            public int ActivityId { get; set; }
            public SolutionType SolutionType { get; set; }
            public string Verdict { get; set; }
        }
    }
}
