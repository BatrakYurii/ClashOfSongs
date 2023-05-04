namespace ClashOfMusic.Api.Models.ViewModels
{
    public class PlayListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int PlayCount { get; set; }
        public string? UserId { get; set; }
        public IEnumerable<string> PreviewImages { get; set; }
        public ICollection<SongViewModel> Songs { get; set; }
    }
}
