using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DataAccessLayer.Models;
using WebApi.Models.DTO;

namespace WebApi.Models.Requests.ActivitySolution
{
    public class ActivitySolutionEditRequestModel
    {
        public int? Id { get; set; }
        public string? SolutionText { get; set; }
        public int? AuthorId { get; set; }
        public string? Verdict { get; set; }
        public int ActivityId { get; set; }
    }
}
