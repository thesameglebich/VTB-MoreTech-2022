using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Requests
{
    public class ReviewsPaginationRequest
    {
        public int Page { get; set; } = 1;
        [Required]
        public int CompanyId { get; set; }
    }
}
