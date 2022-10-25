using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashOfMusic.Api.Data.Entities
{
    public class Song
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string YouTube_Link { get; set; }
        public IEnumerable<PlayListsSongs> PlayListsSongs { get; set; }
        public IEnumerable<GenresSongs> GenresSongs { get; set; }
    }
}
