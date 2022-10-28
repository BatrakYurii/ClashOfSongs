using ClashOfMusic.Api.Services.Abstractions;
using ClashOfMusic.Api.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashOfMusic.Api.Services.Services
{
    public class PlayListServices : IPlayListServices
    {
        public Task<PlayListModel> CreatePlayList(PlayListModel model)
        {
            throw new NotImplementedException();
        }

        public void DeletePlayList(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PlayListModel> GetPlayList(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PlayListModel>> GetPlayLists()
        {
            throw new NotImplementedException();
        }

        public Task<PlayListModel> UpdatePlayList(PlayListModel model)
        {
            throw new NotImplementedException();
        }
    }
}
