using ClashOfMusic.Api.Data.Abstractions;
using ClashOfMusic.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;
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

        public async Task CreateAsync(PlayList playList, List<Song> songs)
        {
            await _ctx.PlayLists.AddAsync(playList);
            await _ctx.Songs.AddRangeAsync(songs);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var playListToRemove = await _ctx.PlayLists.Where(x => x.Id == id).FirstOrDefaultAsync();
            if(playListToRemove != null)
            {
                _ctx.PlayLists.Remove(playListToRemove);
                await _ctx.SaveChangesAsync();
            }
            

        }

        public Task<IEnumerable<PlayList>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<PlayList> GetByIdAsync(int id)
        {
            //var playList = await _ctx.PlayLists.Where(x => x.Id == id).Include(x => x.PlayListsSongs).ThenInclude(x => x.Song).AsNoTracking().FirstOrDefaultAsync();
            //var playList = await _ctx.PlayLists
            //.Where(x => x.Id == id)
            //.Select(x => new PlayList
            //{
            //    Id = x.Id,
            //    Title = x.Title,
            //    Description = x.Description,
            //    PlayListsSongs = x.PlayListsSongs.Select(y => new PlayListsSongs
            //    {
            //        Id = y.Id,
            //        PlayListId = y.PlayListId,
            //        SongId = y.SongId,
            //        Song = new Song
            //        {
            //            Title = y.Song.Title,
            //            YouTube_Link = y.Song.YouTube_Link
            //        }
            //    })
            //})
            //.AsNoTracking()
            //.FirstOrDefaultAsync();
            var playList = await _ctx.PlayLists
            .Where(x => x.Id == id)
            .Select(x => new PlayList
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                PlayListsSongs = x.PlayListsSongs.Select(y => new PlayListsSongs
                {
                    Id = y.Id,
                    PlayListId = y.PlayListId,
                    SongId = y.SongId,
                    Song = new Song
                    {
                        Title = y.Song.Title,
                        YouTube_Link = y.Song.YouTube_Link
                    }
                })
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();
            return playList;
        }

        public Task UpdateAsync(int id, PlayList playList)
        {
            throw new NotImplementedException();
        }
    }
}
