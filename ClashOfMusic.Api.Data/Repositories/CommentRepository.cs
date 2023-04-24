using ClashOfMusic.Api.Data.Abstractions;
using ClashOfMusic.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashOfMusic.Api.Data.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ClashOfMusicContext _ctx;

        public CommentRepository(ClashOfMusicContext ctx)
        {
            _ctx = ctx;
        }

        public async Task CreateComment(Comment commentPostModel)
        {
            await _ctx.Comments.AddAsync(commentPostModel);
        }

        public Task DeleteComment(int commentId, int playListId)
        {
            var playList = _ctx.PlayLists.FirstOrDefaultAsync(x => x.Id == playListId);
        }

        public async Task<IEnumerable<Comment>> GetAllPlayListComments(int id)
        {
            var comments = await _ctx.Comments.Where(x => x.PlayListId == id).ToListAsync();
            return comments;
        }
    }
}
