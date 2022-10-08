using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DataAccessLayer.Models;
using WebApi.Models.DTO;

namespace WebApi.Models.Response.ActivitySolution
{
    public class ActivitySolutionResponseModel
    {
        public int Id { get; set; }
        public string? SolutionText { get; set; }
        public UserDto Author { get; set; }
        public SolutionType SolutionType { get; set; }
        public string? Verdict { get; set; }
    }
}
