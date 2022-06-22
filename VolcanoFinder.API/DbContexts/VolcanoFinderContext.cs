using Microsoft.EntityFrameworkCore;
using VolcanoFinder.API.Models.Entities;

namespace VolcanoFinder.API.DbContexts
{
    public class VolcanoFinderContext : DbContext
    {
        public DbSet<Region> Regions { get; set; } = null!;
        public DbSet<Volcano> Volcanoes { get; set; } = null!;

        public VolcanoFinderContext(DbContextOptions<VolcanoFinderContext> options) : base(options)
        {

        }
    }
}
