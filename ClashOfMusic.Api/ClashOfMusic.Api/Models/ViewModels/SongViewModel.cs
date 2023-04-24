namespace ClashOfMusic.Api.Models.ViewModels
{
    public class SongViewModel
    {
        public string Title { get; set; }
        public string YouTube_Link { get; set; }
        public string? ChannelTitle { get; set; }
        public ulong? ViewCount { get; set; }
        public string? ThumbnailUrl { get; set; }
    }
}
