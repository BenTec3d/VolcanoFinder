using Microsoft.EntityFrameworkCore;
using VolcanoFinder.API.DbContexts;
using VolcanoFinder.API.Models.Entities;

namespace VolcanoFinder.API.Services
{
    public class VolcanoFinderRepository : IVolcanoFinderRepository
    {
        private readonly VolcanoFinderContext _context;

        public VolcanoFinderRepository(VolcanoFinderContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Region>> GetRegionsAsync(bool includeVolcanoes)
        {
            if (includeVolcanoes)
                return await _context.Regions.Include(x => x.Volcanoes).OrderBy(x => x.Name).ToListAsync();

            return await _context.Regions.OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<Region?> GetRegionAsync(int regionId, bool includeVolcanoes)
        {
            if (includeVolcanoes)
                return await _context.Regions.Include(x => x.Volcanoes).Where(x => x.Id == regionId).FirstOrDefaultAsync();

            return await _context.Regions.Where(x => x.Id == regionId).FirstOrDefaultAsync();
        }

        public async Task<bool> RegionExistsAsync(int regionId)
        {
            return await _context.Regions.AnyAsync(x => x.Id == regionId);
        }
        
        public async Task<IEnumerable<Volcano>> GetVolcanoesFromRegionAsync(int regionId)
        {
            return await _context.Volcanoes.Where(x => x.RegionId == regionId).OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<Volcano?> GetVolcanoFromRegionAsync(int regionId, int volcanoId)
        {
            return await _context.Volcanoes.Where(x => x.RegionId == regionId && x.Id == volcanoId).FirstOrDefaultAsync();
        }

        public async Task AddVolcanoToRegionAsync(int regionId, Volcano volcano)
        {
            var region = await GetRegionAsync(regionId, false);

            if(region is not null)
                region.Volcanoes.Add(volcano);
            
            await _context.SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }

    }
}
