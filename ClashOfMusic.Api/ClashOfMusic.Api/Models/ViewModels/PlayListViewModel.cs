namespace ClashOfMusic.Api.Models.ViewModels
{
    public class PlayListViewModel
    {
        public string Title { get; set; }
        public ICollection<SongViewModel> Songs { get; set; }
    }
}
