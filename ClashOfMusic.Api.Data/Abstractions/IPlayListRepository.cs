using ClashOfMusic.Api.Data.Entities;
using ClashOfMusic.Api.Data.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashOfMusic.Api.Data.Abstractions
{
    public interface IPlayListRepository
    {
        public Task<IEnumerable<PlayList>> GetAsync(Pagination pagination, Filter filter);
        public Task<IEnumerable<PlayList>> GetAllByUserIdAsync(string userId);
        public Task<PlayList> GetByIdAsync(int id);
        public Task UpdateAsync(int id, PlayList playList);
        public Task<PlayList> CreateAsync(PlayList playList);
        public Task DeleteAsync(int id);
        public Task IncrementPlayCount(int id);
    }
}
