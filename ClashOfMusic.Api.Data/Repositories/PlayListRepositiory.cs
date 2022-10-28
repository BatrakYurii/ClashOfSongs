using ClashOfMusic.Api.Data.Abstractions;
using ClashOfMusic.Api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashOfMusic.Api.Data.Repositories
{
    public class PlayListRepositiory : IPlayListRepository
    {
        private readonly ClashOfMusicContext _ctx;

        public PlayListRepositiory(ClashOfMusicContext ctx)
        {
            _ctx = ctx;
        }

        public Task<PlayList> CreatePlayList(PlayList playList)
        {
            throw new NotImplementedException();
        }

        public Task DeletePlayList(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PlayList> GetPlayListAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PlayList>> GetPlayListsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PlayList> UpdatePlayList(int id, PlayList playList)
        {
            throw new NotImplementedException();
        }
    }
}
