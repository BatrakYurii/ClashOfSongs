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
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteComment(int commentId, string userId)
        {
            var comment = await _ctx.Comments.FirstOrDefaultAsync(x => x.Id == commentId);
            if(comment.UserId == userId)
            {
                _ctx.Comments.Remove(comment);
                await _ctx.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Comment>> GetAllPlayListComments(int id)
        {
            var comments = await _ctx.Comments.Where(x => x.PlayListId == id).Include(x => x.User).ToListAsync();
            return comments;
        }
    }
}
