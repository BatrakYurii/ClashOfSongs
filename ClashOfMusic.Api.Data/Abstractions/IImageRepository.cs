using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashOfMusic.Api.Data.Abstractions
{
    public interface IImageRepository
    {
        public Task AddImageToProfile(string path, string userId);
        public Task UploadPlayListImages(int userId, List<string> paths);
    }
}
