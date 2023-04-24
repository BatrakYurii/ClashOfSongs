using ClashOfMusic.Api.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashOfMusic.Api.Services.Abstractions
{
    public interface ICommentServices
    {
        public Task<IEnumerable<CommentModel>> GetAllPlayListComments(int id);
        public Task CreateComment(CommentModel commentModel);
        public Task DeleteComment(int commentId, int playListId);
    }
}
