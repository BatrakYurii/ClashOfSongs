using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashOfMusic.Api.Services.Models
{
    public class SongModel
    {
        public string Title { get; set; }
        public string YouTube_Link { get; set; }
        public string? ChannelTitle { get; set; }
        public ulong? ViewCount { get; set; }
        public string? ThumbnailUrl { get; set; }

    }
}
