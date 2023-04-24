using ClashOfMusic.Api.Data.Abstractions;
using ClashOfMusic.Api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashOfMusic.Api.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ClashOfMusicContext _ctx;

        public UserRepository(ClashOfMusicContext ctx)
        {
            _ctx = ctx;
        }

        public Task UpdateUser(User user)
        {
            return null;
        }


    }
}
