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
using VTB_Hakaton.DataAccessLayer.Models;
using VTB_Hakaton.Models.Requests.ShopItem;
using VTB_Hakaton.Models.Response.ShopItem;
using WebApi.DataAccessLayer;
using WebApi.DataAccessLayer.Models;
using WebApi.Models.CommonModels;
using static VTB_Hakaton.Controller.WalletController;

namespace VTB_Hakaton.Controller
{
    [ApiController]
    [Authorize]
    [Route("/shopItem")]
    public class ShopItemController : ControllerBase
    {
        private readonly DB _ctx;
        private readonly UserManager<User> _userManager;
        private const string baseUrl = "https://hackathon.lsp.team/hk";
        private readonly HttpClient _client;

        public ShopItemController(DB ctx, UserManager<User> userManager)
        {
            _ctx = ctx;
            _userManager = userManager;
            _client = new HttpClient();
        }

        [HttpGet("getAllShopItems")]
        public Result<List<ShopItemResponseModel>> GetAllShopItems()
        {
            var response = _ctx.ShopItems
                .Select(x => new ShopItemResponseModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    PriceType = x.PriceType,
                    Price = x.Price,
                    AvailiableCount = x.AvailiableCount,
                    NftIf = x.NftId
                })
                .ToList();

            return new Result<List<ShopItemResponseModel>>(response);
        }
        
        [HttpGet("getShopItem")]
        public Result<ShopItemResponseModel> GetShopItem(int id)
        {
            var item = _ctx.ShopItems.FirstOrDefault(x => x.Id == id);

            if (item == null)
            {
                return new Result<ShopItemResponseModel>(System.Net.HttpStatusCode.NotFound, new Error("Товар не найден"));
            }

            return new Result<ShopItemResponseModel>(new ShopItemResponseModel
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                PriceType = item.PriceType,
                AvailiableCount = item.AvailiableCount,
                NftIf = item.NftId
            });
        }

        [HttpPost("create")]
        public async Task<Result<int>> Create([FromBody] ShopItemRequestModel model)
        {
            var current = await _userManager.GetUserAsync(HttpContext.User);

            // проверить мб на принадлежность до запроса отправки

            if (model.NftId != null)
            {
                var transfer = new TransferNft
                {
                    ToPublicKey = "0x27fF0fEC01610bd78C1F16EE8ac12Fda26A8B94e", // кошелек магазина
                    FromPrivateKey = current.PrivateKey,
                    TokenId = model.NftId.Value
                };

                var content = new StringContent(JsonConvert.SerializeObject(transfer), Encoding.UTF8, "application/json");
                var response = await _client.PostAsync(new Uri($"{baseUrl}/v1/transfers/nft"), content);

                if (!response.IsSuccessStatusCode)
                {
                    return new Result<int>(HttpStatusCode.BadRequest, new Error("Ошибка при Отправлении"));
                }
            }

            var item = new ShopItem
            {
                Name = model.Name,
                Price = model.Price,
                PriceType = model.PriceType,
                AvailiableCount = model.AvailiableCount,
                NftId = model.NftId,
                Owner = current
            };

            _ctx.ShopItems.Add(item);
            _ctx.SaveChanges();

            return new Result<int>(item.Id);
        }

        [HttpPost("edit")]
        public async Task<Result<int>> Edit([FromBody] ShopItemRequestModel model)
        {
            return new Result<int>(0);
            /*
            var current = await _userManager.GetUserAsync(HttpContext.User);

            // проверить мб на принадлежность до запроса отправки

            if (model.NftId != null)
            {
                var transfer = new TransferNft
                {
                    ToPublicKey = "0x27fF0fEC01610bd78C1F16EE8ac12Fda26A8B94e", // кошелек магазина
                    FromPrivateKey = current.PrivateKey,
                    TokenId = model.NftId.Value
                };

                var content = new StringContent(JsonConvert.SerializeObject(transfer), Encoding.UTF8, "application/json");
                var response = await _client.PostAsync(new Uri($"{baseUrl}/v1/transfers/nft"), content);

                if (!response.IsSuccessStatusCode)
                {
                    return new Result<int>(HttpStatusCode.BadRequest, new Error("Ошибка при Отправлении"));
                }
            }

            var item = new ShopItem
            {
                Name = model.Name,
                Price = model.Price,
                PriceType = model.PriceType,
                AvailiableCount = model.AvailiableCount,
                NftId = model.NftId,
                Owner = current
            };

            _ctx.ShopItems.Add(item);
            _ctx.SaveChanges();

            return new Result<int>(item.Id);*/
        }
    }
}
