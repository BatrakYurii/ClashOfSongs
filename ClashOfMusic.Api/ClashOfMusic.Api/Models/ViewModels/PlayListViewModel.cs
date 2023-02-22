namespace ClashOfMusic.Api.Models.ViewModels
{
    public class PlayListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public ICollection<SongViewModel> Songs { get; set; }
    }
}
