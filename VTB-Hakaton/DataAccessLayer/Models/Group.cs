using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DataAccessLayer.Models
{
    public class Group : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public ICollection<Activity> Activities {get;set;}
        public ICollection<User> Users { get; set; }

        public int? LeadId { get; set; }
        public User? Lead { get; set; }
    }
}
