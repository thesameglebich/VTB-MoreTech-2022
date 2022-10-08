using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.DTO;

namespace WebApi.Models.Response.Group
{
    public class GroupResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public ICollection<UserDto> Users { get; set; }
        public UserDto Leader { get; set; }
    }
}
