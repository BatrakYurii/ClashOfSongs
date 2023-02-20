using ClashOfMusic.Api.Data.Entities;
using ClashOfMusic.Api.Services.Abstractions;
using ClashOfMusic.Api.Services.Models;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashOfMusic.Api.Services.Services
{
    public class YoutubeSearchServices : IYoutubeSearchServices
    {
        private readonly IConfiguration _config;
        public YoutubeSearchServices(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<SongModel>> Get(string textParamentr)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = _config.GetSection("ApiKey").Value,
                ApplicationName = this.GetType().ToString()
            });

            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = textParamentr; // Replace with your search term.
            searchListRequest.MaxResults = 8;
            searchListRequest.Type = "video";


            // Call the search.list method to retrieve results matching the specified query term.
            var searchListResponse = await searchListRequest.ExecuteAsync();

            

            var videos = searchListResponse.Items.Select((x, i) => new SongModel { Title = x.Snippet.Title, YouTube_Link = x.Id.VideoId, Id=i + 1}).ToList();
            return videos;
        }
    }
}
