using System.ComponentModel.DataAnnotations;

namespace Blogger.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Please enter a valid Email Id")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter your password")]
        public string Password { get; set; }
    }
}
