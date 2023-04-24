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
    [Authorize]
    public class CommentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICommentServices _commentServices;
        public CommentController(IMapper mapper, ICommentServices commentServices)
        {
            _mapper = mapper;
            _commentServices = commentServices;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetPlayListComments/{id}")]
        public async Task<IEnumerable<CommentViewModel>> GetAllPlayListComments(int id)
        {
            var comments = await _commentServices.GetAllPlayListComments(id);
            return comments.Select(x => _mapper.Map<CommentViewModel>(x)).ToList();
        }

        [HttpPost]
        [Route("Create")]
        public async Task CreateComment(CommentPostModel commentPostModel)
        {
            var commetModel = _mapper.Map<CommentModel>(commentPostModel);
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            commetModel.UserId = userId;
            await _commentServices.CreateComment(commetModel);
        }

        [HttpPost]
        [Route("GetPlayListComments/{id}")]
        public async Task DeleteComment(int commentId, int playListId)
        {
            await _commentServices.DeleteComment(commentId, playListId);
        }
    }
}
