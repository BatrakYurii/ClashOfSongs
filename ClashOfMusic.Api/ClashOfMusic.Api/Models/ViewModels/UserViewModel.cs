using ClashOfMusic.Api.Services.Models;

namespace ClashOfMusic.Api.Models.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public ICollection<PlayListViewModel> PlayLists { get; set; }
    }
}
