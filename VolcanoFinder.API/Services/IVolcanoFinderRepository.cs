using VolcanoFinder.API.Models.Entities;

namespace VolcanoFinder.API.Services
{
    public interface IVolcanoFinderRepository
    {
        Task<IEnumerable<Region>> GetRegionsAsync(bool includeVolcanoes);
        Task<Region?> GetRegionAsync(int regionId, bool includeVolcanoes);
        Task<bool> RegionExistsAsync(int regionId);
        Task<IEnumerable<Volcano>> GetVolcanoesFromRegionAsync(int regionId);
        Task<IEnumerable<Volcano>> GetVolcanoesFromRegionAsync(int regionId, bool? active);
        Task<Volcano?> GetVolcanoFromRegionAsync(int regionId, int volcanoId);
        Task AddVolcanoToRegionAsync(int regionId, Volcano volcano);
        Task<bool> SaveChangesAsync();
        void DeleteVolcano(Volcano volcano);
    }
}
