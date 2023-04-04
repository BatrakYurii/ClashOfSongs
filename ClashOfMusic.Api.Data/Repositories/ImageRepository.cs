using ClashOfMusic.Api.Data.Abstractions;
using ClashOfMusic.Api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashOfMusic.Api.Data.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly ClashOfMusicContext _ctx;

        public ImageRepository(ClashOfMusicContext ctx)
        {
            _ctx = ctx;
        }

        public async Task AddImageToProfile(string path, string userId)
        {
            var user = await _ctx.Users.FindAsync(userId);
            user.AvatarImage = path;
            await _ctx.SaveChangesAsync();
        }

        public async Task UploadPlayListImages(int playListId, List<string> paths)
        {
            var playlist = _ctx.PlayLists.Where(x => x.Id == playListId).FirstOrDefault();
            if(playlist != null)
            {
                foreach (var path in paths)
                {
                    _ctx.Images.Add(new Image() { Path = path, PlayListId = playListId });

                }
            }            

            await _ctx.SaveChangesAsync();
        }
    }
}
