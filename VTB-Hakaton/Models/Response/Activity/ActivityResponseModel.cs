using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DataAccessLayer.Models;
using WebApi.Models.DTO;

namespace WebApi.Models.Response.Activity
{
    public class ActivityResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? GroupId { get; set; }
        public ActivityType ActivityType { get; set; }
        public RewardType RewardType { get; set; }
        public int? NftId { get; set; }
        public float? RewardMoney { get; set; }
        public UserDto? Author { get; set; }
    }
}
