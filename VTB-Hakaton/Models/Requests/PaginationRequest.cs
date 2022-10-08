using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Requests
{
    public class PaginationRequest
    {
        [Required]
        public int Page { get; set; }
    }
}
