using api.multitracks.com.Controllers;
using api.multitracks.com.Model;
using Microsoft.EntityFrameworkCore;

namespace api.multitracks.com.DataAccess
{
    public class MTDBContext : DbContext
    {
        public MTDBContext(DbContextOptions<MTDBContext> options) : base(options) { 
        
        }
        public DbSet<Artist> Artist { get; set; }
        public DbSet<Album> Album { get; set; }
        public DbSet<Song> Song { get; set; }
    }
}
