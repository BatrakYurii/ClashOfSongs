using ClashOfMusic.Api.Services.Abstractions;
using ClashOfMusic.Api.Services.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClashOfMusic.Api.Services.Extentions;
using System.Net;

namespace ClashOfMusic.Api.Services.Services
{
    public class GameServices : IGameServices
    {
        private readonly ISession _session;

        public GameServices(IHttpContextAccessor httpContextAccessor)
        {
            _session = httpContextAccessor.HttpContext.Session;
        }

        public void Choose(string songId)
        {
            try
            {
                var songs = _session.Get<IList<SongModel>>("songs");
                var songToDelete = songs.Where(x => x.YouTube_Link != songId).FirstOrDefault();
                songs.Remove(songToDelete);

                songs.Add(songs.FirstOrDefault());
                songs.RemoveAt(0);

                _session.Set("songs", songs);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public IEnumerable<SongModel> GetPair()
        {
            try
            {
                var songs = _session.Get<IList<SongModel>>("songs");
                if (songs != null)
                {
                    if (songs.Count > 1)
                    {
                        var pair = songs.Take(2).ToList();
                        return pair;
                    }
                    else
                    {
                        return songs;
                    }
                }
                else
                    throw new ArgumentNullException();
            }
            catch (ArgumentNullException e)
            {
                throw new HttpRequestException("session was not created", e, HttpStatusCode.InternalServerError);
            }
            catch (Exception e)
            {
                throw new HttpRequestException("An unexpected error occurred", e, HttpStatusCode.InternalServerError);
            }
        }


        public string Start(PlayListModel playlist)
        {
            var songs = SortInRandomOrder(playlist.Songs.ToList());
            _session.Set<IList<SongModel>>("songs", songs);
            return _session.Id;
        }

        private List<SongModel> SortInRandomOrder(List<SongModel> songs)
        {
            Random random = new Random();
            songs = songs.OrderBy(_ => random.Next()).ToList();
            return songs;
        }
    }
}
