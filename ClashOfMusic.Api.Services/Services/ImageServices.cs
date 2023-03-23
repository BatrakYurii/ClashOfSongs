using ClashOfMusic.Api.Data.Abstractions;
using ClashOfMusic.Api.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ClashOfMusic.Api.Services.Services
{
    public class ImageServices : IImageServices
    {
        private readonly IImageRepository _imageRepository;
        public ImageServices(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public void DeleteImage(string imagePath)
        {
            throw new NotImplementedException();
        }

        public async Task<string> UploadImage(IFormFile image, string path)
        {
            using var readStream = image.OpenReadStream();
            var fileExtension = Path.GetExtension(image.FileName);

            var newFileName = $"{Guid.NewGuid()}{fileExtension}";
            var fullPath = Path.Combine(path, newFileName);
            var buffer = new byte[readStream.Length];

            await readStream.ReadAsync(buffer, 0, buffer.Length);
            await File.WriteAllBytesAsync(fullPath, buffer);
            return newFileName;
        }

        public async Task AddImageToProfile(string path, string userId)
        {
            await _imageRepository.AddImageToProfile(path, userId);
        }

        public async Task UploadPlayListImages(int playListId, List<string> videoIdentifiers)
        {
            var imagePaths = new List<string>();
            var urls = videoIdentifiers.Select(x => $"https://img.youtube.com/vi/{x}/mqdefault.jpg").ToList();
            
            using var client = new WebClient();


            foreach(var url in urls)
            {
                Uri uri = new Uri(url);
                var image = client.DownloadData(uri);
                var path = $"{Guid.NewGuid()}.jpg";
                await File.WriteAllBytesAsync(path, image);
                imagePaths.Add(path);

            }
            await _imageRepository.UploadPlayListImages(playListId, imagePaths);

            return;
        }
    }
}
