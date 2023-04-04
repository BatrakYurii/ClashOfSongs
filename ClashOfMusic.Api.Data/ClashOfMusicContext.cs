using ClashOfMusic.Api.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        public ClashOfMusicContext(DbContextOptions<ClashOfMusicContext> options) : base(options)
        {
            

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
            optionsBuilder.UseSqlServer("Server = localhost; Initial Catalog = ClashOfSongs;Trusted_Connection=true;Encrypt=False;TrustServerCertificate=True");
        }

        public DbSet<PlayList> PlayLists { get; set; }
        public DbSet<Song> Songs { get; set; }        
        public DbSet<PlayListsSongs> PlayListsSongs { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<PlayList>()
        .HasMany(p => p.PlayListsSongs)
        .WithOne(ps => ps.PlayList)
        .HasForeignKey(ps => ps.PlayListId);
            builder.Entity<Song>()
                .HasMany(s => s.PlayListsSongs)
                .WithOne(ps => ps.Song)
                .HasForeignKey(ps => ps.SongId);
        }
    }
}
