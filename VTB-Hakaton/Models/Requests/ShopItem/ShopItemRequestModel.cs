using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VTB_Hakaton.DataAccessLayer.Models;

namespace VTB_Hakaton.Models.Requests.ShopItem
{
    public class ShopItemRequestModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public PriceType PriceType { get; set; }
        public int AvailiableCount { get; set; }
        public int? NftId { get; set; }
    }
}
