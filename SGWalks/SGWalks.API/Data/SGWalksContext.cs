using Microsoft.EntityFrameworkCore;
using SGWalks.API.Models.Domain;

namespace SGWalks.API.Data
{
    public class SGWalksContext : DbContext
    {
        public SGWalksContext(DbContextOptions<SGWalksContext> options) : base(options)
        {
            
        }

        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<WalkDifficulty> WalkDificulty { get; set; }
    }
}
