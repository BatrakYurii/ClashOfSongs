using AutoMapper;
using ClashOfMusic.Api.Models.PostModel;
using ClashOfMusic.Api.Models.ViewModels;
using ClashOfMusic.Api.Services.Abstractions;
using ClashOfMusic.Api.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ClashOfMusic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayListController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPlayListServices _playListServices;
        private readonly IImageServices _imageServices;
        public PlayListController(IMapper mapper, IPlayListServices service, IImageServices imageServices)
        {
            _mapper = mapper;
            _playListServices = service;
            _imageServices = imageServices;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IEnumerable<PlayListViewModel>> Get()
        {
            var playLists = await _playListServices.GetPlayLists();
            return playLists.Select(x => _mapper.Map<PlayListViewModel>(x)).ToList();
        }

        [HttpGet]
        [Route("GetAllByUser/{id}")]
        public async Task<IEnumerable<PlayListViewModel>> GetAllByUserId(string id)
        {
            var playList = await _playListServices.GetAllByUserId(id);
            if (playList == null)
                return null;
            else
            {
                var playListsViewModels = playList.Select(x => _mapper.Map<PlayListViewModel>(x)).Take(4).ToList();
                return playListsViewModels;
            }
            
        }

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<PlayListViewModel> GetById(int id)
        {
            var playList = await _playListServices.GetById(id);
            return _mapper.Map<PlayListViewModel>(playList);
        }

        [Authorize]
        [HttpPost]
        [Route("Create")]
        public async Task<PlayListViewModel> Create([FromBody] PlayListPostModel postModel)
        {
            var model = _mapper.Map<PlayListModel>(postModel);

            model.UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var playList = await _playListServices.Create(model);


            
            if(playList != null)
            {
                await _imageServices.UploadPlayListImages(playList.Id, playList.Songs.Select(x => x.YouTube_Link).ToList());
            }



            return null;
        }

        [Authorize]
        [HttpPut]
        [Route("ChangePlaylist/{id}")]
        public async Task<IEnumerable<PlayListViewModel>> Update([FromBody] PlayListPostModel postModel, int id)
        {

            return null;
        }

        //[Authorize]
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task Delete([FromRoute] int id)
        {
            await _playListServices.DeletePlayList(id);
            
        }

        [HttpPut]
        [Route("Increment/{id}")]
        public async Task IncrementPlayCount([FromRoute] int id)
        {
            await _playListServices.DeletePlayList(id);

        }
    }
}
