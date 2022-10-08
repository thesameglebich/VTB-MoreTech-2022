using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Requests
{
    public class RefreshRequest
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}
