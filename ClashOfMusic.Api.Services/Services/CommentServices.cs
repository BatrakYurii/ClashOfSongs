using AutoMapper;
using ClashOfMusic.Api.Data.Abstractions;
using ClashOfMusic.Api.Data.Entities;
using ClashOfMusic.Api.Services.Abstractions;
using ClashOfMusic.Api.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashOfMusic.Api.Services.Services
{
    public class CommentServices : ICommentServices
    {
        private readonly IMapper _mapper;
        private readonly ICommentRepository _commentRepository;
        public CommentServices(IMapper mapper, ICommentRepository commentRepository)
        {
            _mapper = mapper;
            _commentRepository = commentRepository;
        }
        public async Task CreateComment(CommentModel commentModel)
        {
            commentModel.Created = DateTime.UtcNow;
            var entity = _mapper.Map<Comment>(commentModel);
            await _commentRepository.CreateComment(entity);
        }

        public async Task DeleteComment(int commentId, string userId)
        {
            await _commentRepository.DeleteComment(commentId, userId);
        }

        public async Task<IEnumerable<CommentModel>> GetAllPlayListComments(int id)
        {
            var comments = await _commentRepository.GetAllPlayListComments(id);
            return comments.Select(x => _mapper.Map<CommentModel>(x)).ToList();
        }
    }
}
