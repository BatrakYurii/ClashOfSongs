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
        public Task<IEnumerable<PlayList>> GetAsync();
        public Task<IEnumerable<PlayList>> GetAllByUserIdAsync(string userId);
        public Task<PlayList> GetByIdAsync(int id);
        public Task UpdateAsync(int id, PlayList playList);
        public Task CreateAsync(PlayList playList, List<Song> songs);
        public Task DeleteAsync(int id);
    }
}
