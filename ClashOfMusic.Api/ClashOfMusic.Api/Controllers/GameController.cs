using AutoMapper;
using ClashOfMusic.Api.Models.PostModel;
using ClashOfMusic.Api.Models.ViewModels;
using ClashOfMusic.Api.Services.Abstractions;
using ClashOfMusic.Api.Services.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace ClashOfMusic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameServices _gameServices;
        private readonly IMapper _mapper;
        public GameController(IGameServices gameServices, IMapper mapper)
        {
            _gameServices = gameServices;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("StartGame")]
        public string StartGame([FromBody] PlayListPostModel gamePlaylist)
        {
            try
            {
                var sessionId = _gameServices.Start(_mapper.Map<PlayListModel>(gamePlaylist));
                return sessionId;
                
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }


        [HttpGet]
        [Route("Choose/{songId}")]
        public void Choose(string songId)
        {
            _gameServices.Choose(songId);
        }

        [HttpGet]
        [Route("GetPair")]
        public IEnumerable<SongViewModel> GetPair()
        {
            var pairSongs = _gameServices.GetPair();
            return pairSongs.Select(x => _mapper.Map<SongViewModel>(x)).ToList();
        }
    }
}
