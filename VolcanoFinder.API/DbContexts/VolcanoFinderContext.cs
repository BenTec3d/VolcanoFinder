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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Region>().HasData(
                new Region("Africa")
                {
                    Id = 1
                },
                new Region("Asia")
                {
                    Id = 2
                },
                new Region("Australia")
                {
                    Id = 3
                },
                new Region("Europe")
                {
                    Id = 4
                },
                new Region("North America")
                {
                    Id = 5
                },
                new Region("South America")
                {
                    Id = 6
                },
                new Region("Antarctica")
                {
                    Id = 7
                }
            );

            modelBuilder.Entity<Volcano>().HasData(
                new Volcano("Mount Etna", "https://upload.wikimedia.org/wikipedia/commons/8/8d/Mt_Etna_and_Catania.JPG")
                {
                    Id = 1,
                    Description = "Mount Etna is an active stratovolcano on the east coast of Sicily, Italy, in the Metropolitan City of Catania, between the cities of Messina and Catania.",
                    LastEruption = new DateTime(2022, 02, 11),
                    CountryAlpha2 = "IT",
                    Active = true,
                    RegionId = 4
                },
                new Volcano("Mount Fuji", "https://upload.wikimedia.org/wikipedia/commons/1/1b/080103_hakkai_fuji.jpg")
                {
                    Id = 2,
                    Description = "Mount Fuji is located about 100 km (62 mi) southwest of Tokyo and is visible from there on clear days. Mount Fuji's exceptionally symmetrical cone, which is covered in snow for about five months of the year, is commonly used as a cultural icon of Japan and it is frequently depicted in art and photography, as well as visited by sightseers and climbers.",
                    LastEruption = new DateTime(1708, 02, 24),
                    CountryAlpha2 = "JP",
                    Active = true,
                    RegionId = 2
                },
                new Volcano("Mount Kilimanjaro", "https://upload.wikimedia.org/wikipedia/commons/6/6b/Mt._Kilimanjaro_12.2006.JPG")
                {
                    Id = 3,
                    Description = "Mount Kilimanjaro is a dormant volcano in United Republic of Tanzania. It has three volcanic cones: Kibo, Mawenzi, and Shira. It is the highest mountain in Africa and the highest single free-standing mountain above sea level in the world: 5,895 metres (19,341 ft) above sea level and about 4,900 metres (16,100 ft) above its plateau base.",
                    CountryAlpha2 = "TZ",
                    Active = false,
                    RegionId = 1
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
