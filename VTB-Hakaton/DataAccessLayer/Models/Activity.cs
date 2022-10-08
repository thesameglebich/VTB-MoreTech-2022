using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DataAccessLayer.Models
{
    public class Activity: BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? GroupId { get; set; }
        public Group? Group { get; set; }
        public ActivityType ActivityType { get; set; }
        public RewardType RewardType { get; set; }
        public int? NftId { get; set; }
        public int? RewardMoney { get; set; }
        public int? AuthorId { get; set; }
        public User? Author { get; set; }
        //public ICollection<ActivitySolution> ActivitySolutions { get; set; }
    }

    public enum ActivityType
    {
        Group = 1, // активность группы
        Common = 2 // всеобщая активность для всех пользователей
    }

    public enum RewardType
    {
        NFT = 0,
        DigitalRUB = 1,
        Matic = 2,
        Ruble = 3
    }
}
