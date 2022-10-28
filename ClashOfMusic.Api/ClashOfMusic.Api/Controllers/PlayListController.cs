using AutoMapper;
using ClashOfMusic.Api.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClashOfMusic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayListController : ControllerBase
    {
        private readonly IMapper _mapper;
        public PlayListController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetPlaylists")]
        public async Task<IEnumerable<PlayListViewModel>> GetPlayLists()
        {
            return null;
        }
    }
}
