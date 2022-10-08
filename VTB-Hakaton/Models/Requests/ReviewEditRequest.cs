using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Requests
{
    public class ReviewEditRequest
    {
        public int? Id { get; set; }
        public string Text { get; set; }
        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }
        [Required]
        public int CompanyId { get; set; }
    }
}
