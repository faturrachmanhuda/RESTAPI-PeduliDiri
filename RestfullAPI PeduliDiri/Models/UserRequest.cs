using System.ComponentModel.DataAnnotations;

namespace RestfullAPI_PeduliDiri.Models
{
    public class UserRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
