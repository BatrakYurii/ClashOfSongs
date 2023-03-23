using ClashOfMusic.Api.Configuration;
using ClashOfMusic.Api.Data.Entities;
using ClashOfMusic.Api.Extensions;
using ClashOfMusic.Api.Helpers;
using ClashOfMusic.Api.Models;
using ClashOfMusic.Api.Models.PostModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ClashOfMusic.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly AuthHelper _authHelper;
        public AuthController(UserManager<User> userManager, AuthHelper authHelper)
        {
            _userManager = userManager;
            _authHelper = authHelper;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserPostModel userDetails)
        {
            if (!ModelState.IsValid || userDetails == null || _userManager.FindByEmailAsync(userDetails.Email) == null || _userManager.FindByNameAsync(userDetails.UserName) == null)
            {
                return new BadRequestObjectResult(new { Message = "User Registration Failed" });
            }

            var identityUser = new User() { UserName = userDetails.UserName, Email = userDetails.Email };
            var result = await _userManager.CreateAsync(identityUser, userDetails.Password);

            var roleName = RolesEnum.User.GetEnumDescription();
            await _userManager.AddToRoleAsync(identityUser, roleName);


            if (!result.Succeeded)
            {
                var dictionary = new ModelStateDictionary();
                foreach (var error in result.Errors)
                {
                    dictionary.AddModelError(error.Code, error.Description);
                }

                return new BadRequestObjectResult(new { Message = "User Registration Failed", Errors = dictionary });
            }

            return Ok(new { Message = "User Reigstration Successful" });
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] Login loginCredential)
        {
            User identityUser;

            if (!ModelState.IsValid
                || loginCredential == null
                || (identityUser = await _authHelper.ValidateUserAsync(loginCredential)) == null)
            {
                return new BadRequestObjectResult(new { Message = "Login failed" });
            }
            var roles = await _userManager.GetRolesAsync(identityUser);
            var token = _authHelper.GenerateToken(identityUser, roles);

            return Ok(
                new
                {
                    AccessToken = token
                });
        }
    }
}
