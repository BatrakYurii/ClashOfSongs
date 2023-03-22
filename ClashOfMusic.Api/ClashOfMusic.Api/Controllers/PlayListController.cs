using AutoMapper;
using ClashOfMusic.Api.Models.PostModel;
using ClashOfMusic.Api.Models.ViewModels;
using ClashOfMusic.Api.Services.Abstractions;
using ClashOfMusic.Api.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClashOfMusic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayListController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPlayListServices _playListServices;
        public PlayListController(IMapper mapper, IPlayListServices service)
        {
            _mapper = mapper;
            _playListServices = service;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IEnumerable<PlayListViewModel>> Get()
        {
            return null;
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
                var playListsViewModels = playList.Select(x => _mapper.Map<PlayListViewModel>(x)).ToList();
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

        
        [HttpPost]
        [Route("Create")]
        public async Task<IEnumerable<PlayListViewModel>> Create([FromBody] PlayListPostModel postModel)
        {
            var model = _mapper.Map<PlayListModel>(postModel);

            var resultModel = await _playListServices.Create(model);

            return null;
        }

        [Authorize]
        [HttpPut]
        [Route("ChangePlaylist/{id}")]
        public async Task<IEnumerable<PlayListViewModel>> Update([FromBody] PlayListPostModel postModel, int id)
        {

            return null;
        }

        [Authorize]
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task Delete([FromRoute] int id)
        {
            await _playListServices.DeletePlayList(id);
            
        }
    }
}
