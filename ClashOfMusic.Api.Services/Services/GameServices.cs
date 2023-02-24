﻿using ClashOfMusic.Api.Services.Abstractions;
using ClashOfMusic.Api.Services.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClashOfMusic.Api.Services.Extentions;

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
            var songs = _session.Get<IList<SongModel>>("songs");
            var songToDelete = songs.Where(x => x.YouTube_Link != songId).FirstOrDefault();
            songs.Remove(songToDelete);

            songs.Add(songs.FirstOrDefault());
            songs.RemoveAt(0);

            _session.Set("songs", songs);

        }

        public IEnumerable<SongModel> GetPair()
        {
            var songs = _session.Get<IList<SongModel>>("songs");

            if(songs.Count > 1)
            {
                var pair = songs.Take(2).ToList();
                return pair;
            }
            else
            {
                return songs;
            }
           
        }

        public void Start(PlayListModel playlist)
        {
            var songs = SortInRandomOrder(playlist.Songs.ToList());
            _session.Set<IList<SongModel>>("songs", songs);
        }

        private List<SongModel> SortInRandomOrder(List<SongModel> songs)
        {
            Random random = new Random();
            songs = songs.OrderBy(_ => random.Next()).ToList();
            return songs;
        }
    }
}
