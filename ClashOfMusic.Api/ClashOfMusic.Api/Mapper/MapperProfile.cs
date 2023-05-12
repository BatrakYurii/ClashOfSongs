using ClashOfMusic.Api.Models.PostModel;
using ClashOfMusic.Api.Services.Models;
using AutoMapper;
using ClashOfMusic.Api.Data.Entities;
using ClashOfMusic.Api.Models.ViewModels;
using ClashOfMusic.Api.Services.Models.Parameters;
using ClashOfMusic.Api.Models.QueryParameters;
using ClashOfMusic.Api.Data.Parameters;
using System.Linq.Expressions;

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
            CreateMap<User, UserModel>();
            CreateMap<UserModel, UserViewModel>();

            //Comment mapping
            CreateMap<CommentPostModel, CommentModel>();
            CreateMap<CommentModel, Comment>();
            CreateMap<Comment, CommentModel>();
            CreateMap<CommentModel, CommentViewModel>()
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created.ToString()));

            //Pagination mapping
            CreateMap<PaginationPostModel, PaginationModel>();
            CreateMap<PaginationModel, Pagination>();
            CreateMap<Pagination, PaginationModel>();
            CreateMap<PaginationModel, PaginationViewModel>();


            //Filter mapping
            CreateMap<FilterPostModel, FilterModel>();
            CreateMap<FilterModel, Filter>()
                .ConvertUsing((src, dest, ctx) =>
                {
                    dest = new Filter();
                    dest.Predicates = new List<Expression<Func<PlayList, bool>>>();

                    if(!string.IsNullOrEmpty(src.SearchText))
                    {
                        dest.Predicates.Add(x => x.Title.ToLower().Trim().Contains(src.SearchText.ToLower().Trim())
                      || x.Description.ToLower().Trim().Contains(src.SearchText.ToLower().Trim()));

                    }
                    if (src.Sizes != null && src.Sizes.Length > 0)
                    {
                        dest.Predicates.Add(x => src.Sizes.Any(size => x.PlayListsSongs.Count == size));
                    }
                    if (dest.Predicates.Count == 0)
                    {
                        return null;
                    }

                    return dest;
                });
        }
    }
}
