using ClashOfMusic.Api.Configuration;
using ClashOfMusic.Api.Data.Entities;
using ClashOfMusic.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.ObjectPool;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace ClashOfMusic.Api.Helpers
{
    public class AuthHelper
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtBearerTokenSetting _jwtBearerTokenSetting;
        public AuthHelper(UserManager<User> userManager, JwtBearerTokenSetting jwtBearerTokenSetting)
        {
            _userManager = userManager;
            _jwtBearerTokenSetting = jwtBearerTokenSetting;
        }

        public async Task<User> ValidateUserAsync(Login login)
        {
            var identityUser = await _userManager.FindByEmailAsync(login.Email);

            if(identityUser != null)
            {
                var result = _userManager.PasswordHasher.VerifyHashedPassword(identityUser, identityUser.PasswordHash, login.Password);
                return result == PasswordVerificationResult.Failed ? null : identityUser;
            }

            return null;
        }

        public string GenerateToken(User identityUser, IList<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtBearerTokenSetting.SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, identityUser.UserName.ToString()),
                    new Claim(ClaimTypes.Email, identityUser.Email),
                    new Claim(ClaimTypes.NameIdentifier, identityUser.Id),
                    new Claim(ClaimTypes.Role, string.Join(',', roles))
                }),

                Expires = DateTime.Now.AddSeconds(_jwtBearerTokenSetting.ExpiryTimeInSeconds),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature),
                Audience = _jwtBearerTokenSetting.Audience,
                Issuer = _jwtBearerTokenSetting.Issuer
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
