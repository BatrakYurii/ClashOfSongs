using ClashOfMusic.Api.Data.Entities;
using ClashOfMusic.Api.Services.Abstractions;
using ClashOfMusic.Api.Services.Models;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.AspNetCore.Http;
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
                ApiKey = _config.GetSection("YouTubeApiSetting").GetSection("ApiKey").Value,
                ApplicationName = this.GetType().ToString()
            });

            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = textParamentr; // Replace with your search term.
            searchListRequest.MaxResults = 8;
            searchListRequest.Type = "video";

            var searchListResponse = await searchListRequest.ExecuteAsync();

            var videoIds = string.Join(",", searchListResponse.Items.Select(x => x.Id.VideoId));
            var videoListRequest = youtubeService.Videos.List("snippet,statistics");
            videoListRequest.Id = videoIds;
            var videoListResponse = await videoListRequest.ExecuteAsync();

            var videos = searchListResponse.Items.Select(x =>
            {
                var video = videoListResponse.Items.FirstOrDefault(v => v.Id == x.Id.VideoId);
                var thumbnailUrl = video?.Snippet.Thumbnails.High.Url;
                return new SongModel
                {
                    Title = x.Snippet.Title,
                    YouTube_Link = x.Id.VideoId,
                    ChannelTitle = video?.Snippet.ChannelTitle,
                    ViewCount = video?.Statistics.ViewCount,
                    ThumbnailUrl = thumbnailUrl
                };
            }).ToList();

            return videos;
        }


        //public async Task<IEnumerable<IFormFile>> GetPreviewImages(List<string> urls)
        //{
        //    var youtubeService = new YouTubeService(new BaseClientService.Initializer()
        //    {
        //        ApiKey = _config.GetSection("YouTubeApiSetting").GetSection("ApiKey").Value,
        //        ApplicationName = this.GetType().ToString()
        //    });

        //    foreach(var url in urls)
        //    {
        //        var playListImages = youtubeService.Videos.List("snippet");
        //        playListImages.Id = url;

        //        var responce = await playListImages.ExecuteAsync();
        //        var video = responce.Items.FirstOrDefault();

        //        var thumbnailUrl = video?.Snippet?.Thumbnails?.High?.Url;
        //    }

        //}
    }
}
