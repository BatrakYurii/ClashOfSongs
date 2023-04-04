using AutoMapper;
using ClashOfMusic.Api.Data.Entities;
using ClashOfMusic.Api.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace ClashOfMusic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHost;
        private readonly UserManager<User> _userManager;
        private readonly IImageServices _imageServices;

        public ImageController(IMapper mapper, IWebHostEnvironment webHost, UserManager<User> userManager, IImageServices imageServices)
        {
            _webHost = webHost;
            _userManager = userManager;
            _imageServices = imageServices;
        }

        [Authorize(Roles = "Manager, User")]
        [Route("UserAvatar")]
        [HttpPost]
        public async Task AddOrReplaceProfileImage(IFormFile imgFile)
        {
            if(imgFile == null)
            {
                return;
            }

            var type = Path.GetExtension(imgFile.FileName);

            if(!(type == ".jpg" || type == ".png" || type == ".jpeg"))
            {
                throw new BadImageFormatException("Incorrect file format");
            }

            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByIdAsync(userId);

            if(user == null)
            {
                throw new FileNotFoundException();
            }


            if (user.AvatarImage != null)
            {
                var imagePath = Path.Combine(_webHost.WebRootPath, user.AvatarImage);
                System.IO.File.Delete(imagePath);
            }

            var imageName = await _imageServices.UploadImage(imgFile, Path.Combine(_webHost.WebRootPath, "profileImages"));
            await _imageServices.AddImageToProfile(Path.Combine("profileImages", imageName), user.Id);
        }
    }
}
