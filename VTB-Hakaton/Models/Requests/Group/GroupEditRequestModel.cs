using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.Requests.Group
{
    public class GroupEditRequestModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<int> WorkerIds { get; set; }
        public int LeaderId { get; set; }
    }
}
