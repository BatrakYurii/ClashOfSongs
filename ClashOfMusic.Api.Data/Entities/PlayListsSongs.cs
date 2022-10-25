using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashOfMusic.Api.Data.Entities
{
    public class PlayListsSongs
    {
        public int Id { get; set; }
        public int PlayListId { get; set; }
        public PlayList PlayList { get; set; }
        public int SongId { get; set; }
        public Song Song { get; set; }
    }
}
