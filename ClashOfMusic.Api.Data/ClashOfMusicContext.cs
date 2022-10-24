using ClashOfMusic.Api.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ClashOfMusic.Api.Data
{
    public class ClashOfMusicContext : IdentityDbContext<User>
    {
        public ClashOfMusicContext(DbContextOptions options) : base(options)
        {
            

        }

        public DbSet<PlayList> PlayLists { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Genre>().HasData(
                new Genre
                {
                    Id = 1,
                    Title = "Pop"
                },
                new Genre
                {
                    Id = 2,
                    Title = "Hip-hop"
                },new Genre
                {
                    Id = 3,
                    Title = "Trap"
                },
                new Genre
                {
                    Id = 4,
                    Title = "Rock"
                },
                new Genre
                {
                    Id = 5,
                    Title = "Rhythm and blues"
                },
                new Genre
                {
                    Id = 6,
                    Title = "Disco"
                }
                );
        }
    }
}
