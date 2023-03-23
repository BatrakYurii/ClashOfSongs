using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashOfMusic.Api.Services.Abstractions
{
    public interface IImageServices
    {
        public void DeleteImage(string imagePath);
        public Task<string> UploadImage(IFormFile image, string path);
        public Task AddImageToProfile(string path, string userId);

        public Task UploadPlayListImages(int playListId, List<string> videoIdentifiers);
    }
}
