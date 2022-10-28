using ClashOfMusic.Api.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashOfMusic.Api.Services.Abstractions
{
    public interface IPlayListServices
    {
        public Task<IEnumerable<PlayListModel>> GetPlayLists();
        public Task<PlayListModel> GetPlayList(int id);
        public Task<PlayListModel> CreatePlayList(PlayListModel model);
        public Task<PlayListModel> UpdatePlayList(PlayListModel model);
        public void DeletePlayList(int id);
    }
}
