﻿using ClashOfMusic.Api.Models.PostModel;
using ClashOfMusic.Api.Services.Models;
using AutoMapper;
using ClashOfMusic.Api.Data.Entities;
using ClashOfMusic.Api.Models.ViewModels;

namespace ClashOfMusic.Api.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //PlayList mapping
            CreateMap<PlayListPostModel, PlayListModel>();
                // .ForMember(model => model.CookingTime,
                //opts => opts
                //.MapFrom(entity => entity.CookingTime.ToString("HH:mm")));
            CreateMap<PlayListModel, PlayList>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.PlayListsSongs, opt => opt.MapFrom(src => src.Songs.Select(s => new PlayListsSongs { Song = new Song { Title = s.Title, YouTube_Link = s.YouTube_Link } })));
            ;
            CreateMap<PlayList, PlayListModel>()
                .ForMember(dest => dest.Songs, opt => opt.MapFrom(src => src.PlayListsSongs))
                .ForMember(dest => dest.PreviewImages, opt => opt.MapFrom(src => src.PreviewImages.Select(img => img.Path)));
            CreateMap<PlayListModel, PlayListViewModel>();


            //YoutubeSearch mapping
            CreateMap<SongModel, SongViewModel>();

            //Song mapping
            CreateMap<SongPostModel, SongModel>();
            CreateMap<SongModel, Song>();
            CreateMap<Song, SongModel>();
            CreateMap<PlayListsSongs, SongModel>()
                .ConvertUsing((src, dest, ctx) =>
                {
                    dest = new SongModel { Title = src.Song.Title, YouTube_Link = src.Song.YouTube_Link};
                    return dest;
                });

            //User mapping
            CreateMap<User, UserViewModel>();
            CreateMap<UserPostModel, UserModel>();
            CreateMap<UserModel, User>();

            //Image mapping
            //CreateMap<Image, string>()
            //    .ForMember(dest => dest, opt => opt.MapFrom(src => src.Path));
        }
    }
}
