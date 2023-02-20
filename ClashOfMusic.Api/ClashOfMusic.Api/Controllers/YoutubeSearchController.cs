using AutoMapper;
using ClashOfMusic.Api.Models.ViewModels;
using ClashOfMusic.Api.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ClashOfMusic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YoutubeSearchController : ControllerBase
    {
        private readonly IYoutubeSearchServices _youtubeSearchServices;
        private readonly IMapper _mapper;

        public YoutubeSearchController(IYoutubeSearchServices youtubeSearchServices, IMapper mapper)
        {
            _youtubeSearchServices = youtubeSearchServices;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("Get/{textParamentr}")]
        public async Task<IEnumerable<SongViewModel>> Get(string textParamentr)
        {
            var songModels = await _youtubeSearchServices.Get(textParamentr);

            return songModels.Select(x => _mapper.Map<SongViewModel>(x)).ToList();
        }
    }
}
