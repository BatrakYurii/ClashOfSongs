using ClashOfMusic.Api.Data.Abstractions;
using ClashOfMusic.Api.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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
            
            using var client = new HttpClient();


            foreach(var url in urls)
            {
                Uri uri = new Uri(url);

                // Get the file extension
                var uriWithoutQuery = uri.GetLeftPart(UriPartial.Path);
                var fileExtension = Path.GetExtension(uriWithoutQuery);

                // Create file path and ensure directory exists
                var path = Path.Combine("wwwroot","playListImages", $"{Guid.NewGuid()}.jpg");
                //Directory.CreateDirectory("profileImages");

                // Download the image and write to the file
                var imageBytes = await client.GetByteArrayAsync(uri);
                await File.WriteAllBytesAsync(path, imageBytes);
                imagePaths.Add(path);


                //var image = client.DownloadData(uri);

                
                //var path = Path.Combine("profileImages", $"{Guid.NewGuid()}.jpg");
                //await File.WriteAllBytesAsync(path, image);
                //imagePaths.Add(path);

            }
            var pathForPlayList = imagePaths.Select(x => x.Substring(x.IndexOf('\\') + 1)).ToList();
            await _imageRepository.UploadPlayListImages(playListId, pathForPlayList);

            return;
        }
    }
}
