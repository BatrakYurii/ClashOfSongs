using ClashOfMusic.Api.Configuration.Abstractions;
using ClashOfMusic.Api.Data;
using ClashOfMusic.Api.Data.Entities;
using ClashOfMusic.Api.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ClashOfMusic.Api.Configuration.Seeding
{
    public class SeedDataToDB : ISeedDataToDB
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ClashOfMusicContext _context;

        public SeedDataToDB(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ClashOfMusicContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager; 
            _context = context;
        }

        public async Task SeedNeccessaryData()
        {
            await CreateRoles();
            await CreateAdmin();
        }

        private async Task CreateAdmin()
        {
            var roleName = RolesEnum.Admin.GetEnumDescription();
            var adminIfExist = await _userManager.GetUsersInRoleAsync(roleName); 

            if(adminIfExist.Count == 0)
            {
                var admin = new User
                {
                    UserName = "Admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin1@clashofsongs.com",
                    NormalizedEmail = "ADMIN1@CLAHOFSONGS.COM",
                    EmailConfirmed = true,
                    PhoneNumber = "380975139234"
                };

                await _userManager.CreateAsync(admin, "administrator");
                await _userManager.AddToRoleAsync(admin, roleName);
            }
        }

        private async Task CreateRoles()
        {
            //var isAdminRoleExist =  await _roleManager.FindByNameAsync(RolesEnum.Admin.GetEnumDescription());

            //if(isAdminRoleExist != null)
            //{
            //    var admin = new IdentityRole("Admin");
            //    admin.NormalizedName = admin.Name.ToUpper();
            //    await _roleManager.CreateAsync(admin);
            //    _context.
            //}

            //var isUserRoleExist = await _roleManager.FindByNameAsync(RolesEnum.User.GetEnumDescription());

            //if (isUserRoleExist != null)
            //{
            //    var user = new IdentityRole("User");
            //    user.NormalizedName = user.Name.ToUpper();
            //    await _roleManager.CreateAsync(user);
            //}


            string[] roles = new string[] { "Admin", "User" };

            foreach (string role in roles)
            {
                var roleStore = new RoleStore<IdentityRole>(_context);

                if (!_context.Roles.Any(r => r.Name == role))
                {
                    await roleStore.CreateAsync(new IdentityRole(role)
                    {
                        NormalizedName = role.ToUpper()
                    }); 
                }
            }
        }
    }
}
