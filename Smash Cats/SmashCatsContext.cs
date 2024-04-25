using Microsoft.EntityFrameworkCore;
using Smash_Cats.Models;

namespace Smash_Cat
{
    public class SmashCatsContext : DbContext
    {
        public  SmashCatsContext(DbContextOptions<SmashCatsContext> options) : base(options)
        {

        }

        public DbSet<Room> Rooms { get; set; }  

    }
}
