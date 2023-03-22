using System.ComponentModel.DataAnnotations;

namespace ClashOfMusic.Api.Models.PutModels
{
    public class UserPutModel
    {
        public string UserName { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
