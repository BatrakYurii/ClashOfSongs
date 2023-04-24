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

        public async Task<PlayList> CreateAsync(PlayList playList)
        {
            try
            {
                var songsFromModel = playList.PlayListsSongs.Select(x => x.Song.YouTube_Link).ToList();
                var songsFromDb = _ctx.Songs.Select(x => x.YouTube_Link).ToList();
                var anuniqueSongsId = songsFromModel.Where(x => songsFromDb.Contains(x)).ToList();

                foreach (var el in anuniqueSongsId)
                {
                    var playListsSongsModel = playList.PlayListsSongs.Where(x => x.Song.YouTube_Link == el).FirstOrDefault();
                    playListsSongsModel.SongId = playListsSongsModel.Song.YouTube_Link;
                    playListsSongsModel.Song = null;

                }
                //playList.PlayListsSongs.Where(x => anuniqueSongsId.Contains(x.SongId)).ToList();

                await _ctx.PlayLists.AddAsync(playList);
                await _ctx.SaveChangesAsync();


            }
            catch (InvalidOperationException e)
            {
                throw new Exception("PlayList have to have onlly unique entries", e);
            }
            catch (Exception)
            {

            }
            var dataForImages = new PlayList { Id = playList.Id, PlayListsSongs = playList.PlayListsSongs.Where(x => x.Song != null).Take(4).ToList() };
            return dataForImages;
        }

        public async Task DeleteAsync(int id)
        {
            var playListToRemove = await _ctx.PlayLists.Where(x => x.Id == id).FirstOrDefaultAsync();
            if(playListToRemove != null)
            {
                var connectModels = await _ctx.PlayListsSongs.Where(x => x.PlayListId == playListToRemove.Id).Select(x => x.SongId).ToListAsync();

                _ctx.PlayLists.Remove(playListToRemove);
                await _ctx.SaveChangesAsync();


                var toDelete = connectModels.Where(x => _ctx.PlayListsSongs.Where(y => y.SongId == x).FirstOrDefault() == null).ToList();

                foreach(var el in toDelete)
                {
                    var song = await _ctx.Songs.Where(x => x.YouTube_Link == el).FirstOrDefaultAsync();
                    _ctx.Songs.Remove(song);
                }               

                await _ctx.SaveChangesAsync();
            }
            

        }

        public async Task<IEnumerable<PlayList>> GetAllByUserIdAsync(string userId)
        {
            var usersPlaylists = await _ctx.PlayLists.Where(x => x.UserId == userId).Include(x => x.PreviewImages).ToListAsync();
            return usersPlaylists;
        }

        public async Task<IEnumerable<PlayList>> GetAsync()
        {
            var playLists = await _ctx.PlayLists.Take(100).Include(x => x.PreviewImages).Include(x => x.PlayListsSongs).ThenInclude(x => x.Song).ToListAsync();
            return playLists;
        }

        public async Task<PlayList> GetByIdAsync(int id)
        {
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
                }).ToList()
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();
            return playList;
        }

        public async Task IncrementPlayCount(int id)
        {
            var playList = await _ctx.PlayLists.FirstOrDefaultAsync(x => x.Id == id);
            
            if(playList != null)
            {
                playList.PlayCount++;

                await _ctx.SaveChangesAsync();
            }
        }

        public Task UpdateAsync(int id, PlayList playList)
        {
            throw new NotImplementedException();
        }
    }
}
