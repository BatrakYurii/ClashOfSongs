using ClashOfMusic.Api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashOfMusic.Api.Data.Abstractions
{
    public interface ICommentRepository
    {
        public Task<IEnumerable<Comment>> GetAllPlayListComments(int id);
        public Task CreateComment(Comment commentPostModel);
        public Task DeleteComment(int commentId, int playListId);
    }
}
