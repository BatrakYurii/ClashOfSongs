using ClashOfMusic.Api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashOfMusic.Api.Data.Abstractions
{
    public interface IPlayListRepository
    {
        public Task<IEnumerable<PlayList>> GetPlayListsAsync();
        public Task<PlayList> GetPlayListAsync(int id);
        public Task<PlayList> UpdatePlayList(int id, PlayList playList);
        public Task<PlayList> CreatePlayList(PlayList playList);
        public Task DeletePlayList(int id);
    }
}
