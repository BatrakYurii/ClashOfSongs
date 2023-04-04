using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashOfMusic.Api.Data.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public int PlayListId { get; set; }
        public PlayList PlayList { get; set; }
    }
}
