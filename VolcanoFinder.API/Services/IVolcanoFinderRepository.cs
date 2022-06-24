using VolcanoFinder.API.Models.Entities;
using VolcanoFinder.API.Models.Metadata;

namespace VolcanoFinder.API.Services
{
    public interface IVolcanoFinderRepository
    {
        Task<(IEnumerable<Region>, PaginationMetadata)> GetRegionsAsync(bool includeVolcanoes, int pageNumber, int pageSize);
        Task<Region?> GetRegionAsync(int regionId, bool includeVolcanoes);
        Task<bool> RegionExistsAsync(int regionId);
        Task<(IEnumerable<Volcano>, PaginationMetadata)> GetVolcanoesFromRegionAsync(int regionId, bool? active, string? searchQuery, int pageNumber, int pageSize);
        Task<Volcano?> GetVolcanoFromRegionAsync(int regionId, int volcanoId);
        Task AddVolcanoToRegionAsync(int regionId, Volcano volcano);
        Task<bool> SaveChangesAsync();
        void DeleteVolcano(Volcano volcano);
        Task<IEnumerable<User>> GetUsersAsync(string name);
    }
}
