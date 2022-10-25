using System.ComponentModel.DataAnnotations;

namespace ClashOfMusic.Api.Models.PostModel
{
    public class UserPostModel
    {
        [Required]
        public string UserName { get; set; }
        
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
