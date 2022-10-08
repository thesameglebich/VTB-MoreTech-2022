using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DataAccessLayer.Models;
using WebApi.Models.DTO;

namespace WebApi.Models.Requests.Activity
{
    public class ActivityEditRequestModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? GroupId { get; set; }
        public ActivityType ActivityType { get; set; }
        public RewardType RewardType { get; set; }
        public int? NftId { get; set; }
        public int? RewardMoney { get; set; }
        public int AuthorId { get; set; }
    }
}
