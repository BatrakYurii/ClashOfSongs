using ClashOfMusic.Api.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashOfMusic.Api.Services.Abstractions
{
    public interface IGameServices
    {
        public IEnumerable<SongModel> GetPair();
        public void Choose(string songId);
        public void Start(PlayListModel playlist);
    }
}
