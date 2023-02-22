namespace ClashOfMusic.Api.Models.PostModel
{
    public class PlayListPostModel
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public ICollection<SongPostModel> Songs { get; set; }
    }
}
