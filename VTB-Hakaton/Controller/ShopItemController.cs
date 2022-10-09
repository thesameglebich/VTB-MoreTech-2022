using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using VTB_Hakaton.Models.Response.ShopItem;
using WebApi.DataAccessLayer;
using WebApi.DataAccessLayer.Models;
using WebApi.Models.CommonModels;

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
                    Price = x.Price
                })
                .ToList();

            return new Result<List<ShopItemResponseModel>>(response);
        }
        /*
        [HttpGet("getShopItem")]
        public Result<ShopItemResponseModel> GetShopItem(int id)
        {
            var item = _ctx.ShopItems.FirstOrDefault(x => x.Id);

            if (item == null)
            {
                return new Result<ShopItemResponseModel>(System.Net.HttpStatusCode.NotFound, new Error("Товар не найден"));
            }


        }

        [HttpGet("getAllShopItems")]
        public Result GetAllShopItems()
        {

        }

        [HttpGet("getAllShopItems")]
        public Result GetAllShopItems()
        {

        }

        [HttpGet("getAllShopItems")]
        public Result GetAllShopItems()
        {

        }

        [HttpGet("getAllShopItems")]
        public Result GetAllShopItems()
        {

        }*/
    }
}
