using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Requests
{
    public class NotificationSendRequest
    {
        public int[] Students { get; set; }
        public int[] Companies { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
