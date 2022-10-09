using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DataAccessLayer.Models;

namespace VTB_Hakaton.DataAccessLayer.Models
{
    public class ShopItem: BaseEntity
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public PriceType PriceType { get; set; }
        public int AvailiableCount { get; set; }
        public int OwnerId { get; set; }
        public User Owner { get; set; }
        public int? NftId { get; set; }
    }

    public enum PriceType
    {
        DigitalRub = 0,
        Matic = 1
    }
}
