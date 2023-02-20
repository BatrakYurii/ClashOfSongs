using ClashOfMusic.Api.Models.PostModel;
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
            CreateMap<PlayListPostModel, PlayListModel>()
                 .ForMember(model => model.CookingTime,
                opts => opts
                .MapFrom(entity => entity.CookingTime.ToString("HH:mm")));
            CreateMap<PlayListModel, PlayList>();

            //YoutubeSearch mapping
            CreateMap<SongModel, SongViewModel>();
        }
    }
}
