using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApi.DataAccessLayer;
using WebApi.DataAccessLayer.Models;
using WebApi.Models.CommonModels;
using WebApi.Models.Requests.ActivitySolution;
using WebApi.Models.Response.ActivitySolution;
using static VTB_Hakaton.Controller.WalletController;

namespace WebApi.Controller
{
    [ApiController]
    [Authorize]
    [Route("/solution")]
    public class ActivitySolutionController : ControllerBase
    {
        private readonly DB _ctx;
        private readonly UserManager<User> _userManager;
        private const string baseUrl = "https://hackathon.lsp.team/hk";
        private readonly HttpClient _client;

        public ActivitySolutionController(DB ctx, UserManager<User> userManager)
        {
            _ctx = ctx;
            _userManager = userManager;
            _client = new HttpClient();
        }

        [HttpGet("getAllActivitySolutions")]
        public Result<List<ActivitySolutionResponseModel>> GetAllActivitySolutions(int activityId)
        {
            var solutions = _ctx.ActivitySolutions
                .Include(x => x.Author)
                .Where(x => x.ActivityId == activityId)
                .Select(x => new ActivitySolutionResponseModel
                {
                    Id = x.Id,
                    SolutionText = x.SolutionText,
                    Author = new Models.DTO.UserDto
                    {
                        Id = x.AuthorId.Value,
                        Email = x.Author.Email,
                        FullName = x.Author.FullName
                    },
                    SolutionType = x.SolutionType,
                    Verdict = x.Verdict
                })
                .ToList();

            return new Result<List<ActivitySolutionResponseModel>>(solutions);
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
            var solution = _ctx.ActivitySolutions
                .Include(x => x.Activity)
                .ThenInclude(a => a.Author)
                .Include(x => x.Author)
                .FirstOrDefault(x => x.Id == model.ActivityId);

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

            if (solution.SolutionType == SolutionType.Approved)
            {
                SendReward(solution);
            }

            return new Result(HttpStatusCode.OK);
        }

        private async void SendReward(ActivitySolution solution)
        {
            if (solution.Activity.RewardType == RewardType.Matic)
            {
                var transfer = new TransferCurrency
                {
                    ToPublicKey = solution.Author.PublicKey,
                    FromPrivateKey = solution.Activity.Author.PrivateKey,
                    Amount = solution.Activity.RewardMoney.Value
                };

                var content = new StringContent(JsonConvert.SerializeObject(transfer), Encoding.UTF8, "application/json");
                await _client.PostAsync(new Uri($"{baseUrl}/v1/transfers/matic"), content);
            }
            if (solution.Activity.RewardType == RewardType.Matic)
            {
                var transfer = new TransferCurrency
                {
                    ToPublicKey = solution.Author.PublicKey,
                    FromPrivateKey = solution.Activity.Author.PrivateKey,
                    Amount = solution.Activity.RewardMoney.Value
                };

                var content = new StringContent(JsonConvert.SerializeObject(transfer), Encoding.UTF8, "application/json");
                await _client.PostAsync(new Uri($"{baseUrl}/v1/transfers/ruble"), content);
            }
            if (solution.Activity.RewardType == RewardType.Matic)
            {
                var transfer = new TransferNft
                {
                    ToPublicKey = solution.Author.PublicKey,
                    FromPrivateKey = solution.Activity.Author.PrivateKey,
                    TokenId = solution.Activity.NftId.Value
                };

                var content = new StringContent(JsonConvert.SerializeObject(transfer), Encoding.UTF8, "application/json");
                await _client.PostAsync(new Uri($"{baseUrl}/v1/transfers/nft"), content);
            }
        }

        public class SetSolutionRequestModel
        {
            public int ActivityId { get; set; }
            public SolutionType SolutionType { get; set; }
            public string Verdict { get; set; }
        }
    }
}
