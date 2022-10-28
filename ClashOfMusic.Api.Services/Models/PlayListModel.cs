using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashOfMusic.Api.Services.Models
{
    public class PlayListModel
    {
        public int Id { get; set; }
        public ICollection<SongModel> Songs { get; set; }
    }
}
