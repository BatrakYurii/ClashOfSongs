using ClashOfMusic.Api.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashOfMusic.Api.Services.Abstractions
{
    public interface IUserServices
    {
        public Task UpdateUser(UserModel model);
    }
}
