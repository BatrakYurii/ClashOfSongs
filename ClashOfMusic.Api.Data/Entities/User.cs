using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashOfMusic.Api.Data.Entities
{
    public class User : IdentityUser
    {
        public string UserName { get; set; }
        public ICollection<PlayList> PlayLists { get; set; }
        public string AvatarImage { get; set; }
        public int Like { get; set; }
        public int Dislike { get; set; }
        
    }
}
