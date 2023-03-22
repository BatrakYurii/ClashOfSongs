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
        public Task<PlayListModel> GetById(int id);
        public Task<IEnumerable<PlayListModel>> GetAllByUserId(string userId);
        public Task<PlayListModel> Create(PlayListModel model);
        public Task<PlayListModel> Update(PlayListModel model, int id);
        public Task DeletePlayList(int id);
    }
}
