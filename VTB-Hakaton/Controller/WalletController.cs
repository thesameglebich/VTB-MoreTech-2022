using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VTB_Hakaton.Models.Requests.Wallet;
using WebApi.DataAccessLayer;
using WebApi.DataAccessLayer.Models;
using WebApi.Models.CommonModels;

namespace VTB_Hakaton.Controller
{
    [ApiController]
    [Authorize]
    [Route("/wallet")]
    public class WalletController : ControllerBase
    {
        private readonly DB _ctx;
        private readonly UserManager<User> _userManager;
        private const string baseUrl = "https://hackathon.lsp.team/hk";
        private readonly HttpClient _client;

        public WalletController(DB ctx, UserManager<User> userManager)
        {
            _ctx = ctx;
            _userManager = userManager;
            _client = new HttpClient();
        }

        #region осуществление транзакий

        [HttpPost("sendMaticAsync")]
        public async Task<Result> SendMaticAsync([FromBody]TransferCurrencyRequestModel model)
        {
            var current = await _userManager.GetUserAsync(HttpContext.User);

            var toWallet = _ctx.Users.FirstOrDefault(x => x.Email == model.Email).Email;

            if (string.IsNullOrWhiteSpace(toWallet))
            {
                return new Result(HttpStatusCode.NotFound, new Error("Пользователь не найден"));
            }

            var transfer = new TransferCurrency
            {
                ToPublicKey = toWallet,
                FromPrivateKey = current.PrivateKey,
                Amount = model.Amount
            };

            var content = new StringContent(JsonConvert.SerializeObject(transfer), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(new Uri($"{baseUrl}/v1/transfers/matic"), content);

            if (!response.IsSuccessStatusCode)
            {
                return new Result(HttpStatusCode.BadRequest, new Error("Ошибка при Отправлении"));
            }

            var stringResponse = await response.Content.ReadAsStringAsync();

            //var keys = JsonConvert.DeserializeObject<Keys>(stringResponse);

            return new Result(HttpStatusCode.OK);
        }

        [HttpPost("sendRubleAsync")]
        public async Task<Result> SendRubleAsync([FromBody]TransferCurrencyRequestModel model)
        {
            var current = await _userManager.GetUserAsync(HttpContext.User);

            var toWallet = _ctx.Users.FirstOrDefault(x => x.Email == model.Email).Email;

            if (string.IsNullOrWhiteSpace(toWallet))
            {
                return new Result(HttpStatusCode.NotFound, new Error("Пользователь не найден"));
            }

            var transfer = new TransferCurrency
            {
                ToPublicKey = toWallet,
                FromPrivateKey = current.PrivateKey,
                Amount = model.Amount
            };

            var content = new StringContent(JsonConvert.SerializeObject(transfer), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(new Uri($"{baseUrl}/v1/transfers/ruble"), content);

            if (!response.IsSuccessStatusCode)
            {
                return new Result(HttpStatusCode.BadRequest, new Error("Ошибка при Отправлении"));
            }

            var stringResponse = await response.Content.ReadAsStringAsync();

            //var keys = JsonConvert.DeserializeObject<Keys>(stringResponse);

            return new Result(HttpStatusCode.OK);
        }

        [HttpPost("sendNftAsync")]
        public async Task<Result> SendNftAsync([FromBody]TransferNftRequestModel model)
        {
            var current = await _userManager.GetUserAsync(HttpContext.User);

            var toWallet = _ctx.Users.FirstOrDefault(x => x.Email == model.Email).Email;

            if (string.IsNullOrWhiteSpace(toWallet))
            {
                return new Result(HttpStatusCode.NotFound, new Error("Пользователь не найден"));
            }

            var transfer = new TransferNft
            {
                ToPublicKey = toWallet,
                FromPrivateKey = current.PrivateKey,
                TokenId = model.NftId
            };

            var content = new StringContent(JsonConvert.SerializeObject(transfer), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(new Uri($"{baseUrl}/v1/transfers/nft"), content);

            if (!response.IsSuccessStatusCode)
            {
                return new Result(HttpStatusCode.BadRequest, new Error("Ошибка при Отправлении"));
            }

            var stringResponse = await response.Content.ReadAsStringAsync();

            //ToDo создать транзакцию как сущность для показа пользователям информации!!! 
            // ToDO обновлять статус для всех транзакций где ещё нет Success джобой, раз в минуту

            //var keys = JsonConvert.DeserializeObject<Keys>(stringResponse);

            return new Result(HttpStatusCode.OK);
        }

        [HttpPost("createNft")]
        public async Task<Result> CreateNft([FromBody] NftCollectionRequestModel model)
        {
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(new Uri($"{baseUrl}/v1/transfers/nft"), content);

            if (!response.IsSuccessStatusCode)
            {
                return new Result(HttpStatusCode.BadRequest, new Error("Ошибка при Отправлении"));
            }

            return new Result(HttpStatusCode.OK);
        }

        #endregion

        public class TransferCurrency
        {
            public string FromPrivateKey { get; set; }
            public string ToPublicKey { get; set; }
            public float Amount { get; set; }
        }

        public class TransferNft
        {
            public string FromPrivateKey { get; set; }
            public string ToPublicKey { get; set; }
            public int TokenId { get; set; }
        }

        public class NftCollectionRequestModel
        {
            public string ToPublicKey { get; set; }
            public string Uri { get; set; }
            public int NftCount { get; set; }
        }
    }
}
