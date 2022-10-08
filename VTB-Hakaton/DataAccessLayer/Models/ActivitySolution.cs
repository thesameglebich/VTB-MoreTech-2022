using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DataAccessLayer.Models
{
    public class ActivitySolution : BaseEntity
    {
        public string? SolutionText { get; set; }
        public string? SolutionFilePath { get; set; }
        public int AuthorId { get; set; }
        public User Author { get; set; }
        public SolutionType SolutionType { get; set; }
        public string? Verdict { get; set; }
        public int ActivityId { get; set; }
        public Activity Activity { get; set; }
    }

    public enum SolutionType
    {
        WaitingApprove = 0,
        Approved = 1,
        Declined = 2
    }
}
